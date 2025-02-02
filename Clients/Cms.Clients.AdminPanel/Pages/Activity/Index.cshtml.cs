using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;
using Cms.Clients.AdminPanel.Auth;

namespace Cms.Clients.AdminPanel.Pages.Activity
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<NewsViewModel> ActivityList { get; set; }
        public List<LanguageViewModel> Languages { get; set; }


        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public ActionResult OnGet(string searchText = "", int orderById = 0)
        {
            // _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);
            var config = (IConfiguration)HttpContext.RequestServices.GetRequiredService(typeof(IConfiguration));

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion


            var data = new { pageNumber = 1, pageSize = 200, typeId = config.GetSection("NewsId").GetSection("NewsActivityId").Value, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            ActivityList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

            if (!string.IsNullOrEmpty(searchText))
            {
                ActivityList = ActivityList.Where(t => t.Title.Contains(searchText) || t.Text.Contains(searchText)).ToList();
            }
            if (orderById == 1)
            {
                ActivityList = ActivityList.OrderBy(t => t.Title).ToList();
            }

            return Page();
        }
    }
}
