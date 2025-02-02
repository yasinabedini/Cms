
using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Sweeper
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;        

        [BindProperty]
        public SweeperViewModel Sweeper { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");            
        }

        public List<LanguageViewModel> Languages { get; set; }

        public void OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region Fill Sweeper
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Sweeper/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;
            #endregion

            #region Languages              
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion

            Sweeper = JsonConvert.DeserializeObject<SweeperViewModel>(modelResult);
        }

        public async Task<IActionResult> OnPost(IFormFile? image)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            if (!ModelState.IsValid)
            {

                return Page();
            }

            #region Save Image
            if (image is not null)
            {
                _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={Sweeper.ImageName}&&folder=sweeper");

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                image.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(image.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=sweeper", requestContent).Result;
                Sweeper.ImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }
            #endregion

            var modelData = new { Id = Sweeper.Id, Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, ImageName = Sweeper.ImageName, IsEnable = Sweeper.IsEnable, LanguageId = Sweeper.LanguageId };
            
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse =await _httpClient.PutAsync("/api/Sweeper/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
