using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Activity
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;


        [BindProperty]
        public NewsViewModel Activity { get; set; }
        public List<NewsViewModel> ActivityList { get; set; }
        public List<LanguageViewModel> Languages { get; set; }


        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");

        }

        public IActionResult OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            #region Activity List
            var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            ActivityList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;
            #endregion

            var silngeData = new { Id = id }; // Your data object

            jsonInString = JsonConvert.SerializeObject(silngeData);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            response = _httpClient.PostAsync("/api/News/GetById", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            result = response.Content.ReadAsStringAsync().Result;

            Activity = JsonConvert.DeserializeObject<NewsViewModel>(result);

            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile? mainImage)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            #region Activity List
            var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            ActivityList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;
            #endregion

            if (!ModelState.IsValid)
            {

                return Page();
            }

            #region Update Gallery
            if (mainImage is not null)
            {
                var deleteResult = _fileManager.DeleteAsync($"/api/FileManager/Delete?imageName={Activity.MainImageName}&&folder=news").Result;
                if (deleteResult.IsSuccessStatusCode)
                {
                    var requestContent = new MultipartFormDataContent();
                    var item = new MemoryStream();
                    mainImage.CopyTo(item);
                    item.Position = 0;
                    var imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "image", Path.GetFileName(mainImage.FileName));
                    var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

                    Activity.MainImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
                }
            }

            #endregion

            var modelData = new { Id = Activity.Id, Title = Activity.Title, Introduction = Activity.Introduction, LanguageId = Activity.LanguageId, NewsTypeId = Activity.NewsTypeId,IsEnable = Activity.IsEnable ,PublishDate = Activity.PublishDate, Text = Activity.Text, MainImage = Activity.MainImageName, SecondImage = Activity.SecondImage, ThirdImage = Activity.ThirdImage };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

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
