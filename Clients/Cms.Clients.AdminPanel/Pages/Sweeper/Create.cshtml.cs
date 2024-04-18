using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Cms.Clients.AdminPanel.Auth;

namespace Cms.Clients.AdminPanel.Pages.Sweeper
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;

        [BindProperty]
        public SweeperViewModel Sweeper { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");
        }

        public List<LanguageViewModel> Languages { get; set; }

        public void OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

        }

        public IActionResult OnPost(IFormFile image)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            if (!ModelState.IsValid)
            {
                if (image is null)
                {
                    ModelState.AddModelError("image", "یک عکس برای Sweeper اپلود کنید");
                }
                var data = new { pageNumber = 1, pageSize = 200 };
                var jsonInString = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

                return Page();
            }

            #region Save Images
            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            image.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", Path.GetFileName(image.FileName));
            var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=sweeper", requestContent).Result;
            Sweeper.ImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
            #endregion


            var modelData = new { Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, Image = Sweeper.ImageName, LanguageId = Sweeper.LanguageId };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/Sweeper/Create", modelContent).Result;

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
