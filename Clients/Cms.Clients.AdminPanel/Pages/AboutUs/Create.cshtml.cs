using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.AboutUs
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;


        [BindProperty]
        public NewsViewModel About { get; set; }
        public List<NewsViewModel> AboutList { get; set; }
        public List<LanguageViewModel> Languages { get; set; }


        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");

        }
        public IActionResult OnGet()
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

            var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            AboutList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile mainImage)
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

            if (!ModelState.IsValid)
            {
                if (mainImage is null)
                {
                    ModelState.AddModelError("mainImage", "یک عکس برای فعالیت اپلود کنید");
                }

                var data = new { pageNumber = 1, pageSize = 200 };
                var jsonInString = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

                return Page();
            }

            #region Save Main Image
            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            mainImage.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", Path.GetFileName(mainImage.FileName));
            var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

            About.MainImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
            #endregion

            var modelData = new { Title = About.Title, Introduction = About.Introduction, LanguageId = About.LanguageId, NewsTypeId = About.NewsTypeId, PublishDate = About.PublishDate, Text = About.Text, MainImage = About.MainImageName, SecondImage = About.SecondImage, ThirdImage = About.ThirdImage }; // Your data object

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/News/Create", modelContent);

            if (methodresponse.Result.IsSuccessStatusCode)
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
