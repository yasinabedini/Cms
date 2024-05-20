using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.AboutUs
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<NewsViewModel> AboutList { get; set; }
        
        public List<NewsTypeViewModel> NewsTypes { get; set; }
        public List<LanguageViewModel> Languages { get; set; }


        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public async Task<ActionResult> OnGet()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);


            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse =await _httpClient.PostAsync("/api/Language/GetAll", languageContent);
            var languageResult =await languageResponse.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            var newsTypeData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(newsTypeData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => t.IsPage).ToList();


            var data = new { pageNumber = 1, pageSize = 400, typeId = 0, isPage = true }; // Your data object

             jsonInString = JsonConvert.SerializeObject(data);
             content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

             response =await _httpClient.PostAsync("/api/News/GetAll", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
             result =await response.Content.ReadAsStringAsync();

            AboutList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

            return Page();
        }
    }
}
