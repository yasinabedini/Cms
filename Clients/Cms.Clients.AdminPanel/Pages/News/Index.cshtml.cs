using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;
using IdentityModel.Client;
using Cms.Clients.AdminPanel.Auth;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<NewsViewModel> NewsList { get; set; }
        public List<NewsTypeViewModel> NewsTypes { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public async Task<IActionResult> OnGet(int pageNumber = 1, int typeId = 0, int LanguageId = 0, string searchText = "")
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var newsTypeData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(newsTypeData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response =await _httpClient.PostAsync("/api/NewsType/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            var data = new { pageNumber, pageSize = 200, typeId = typeId, languageId = LanguageId, isPage = false }; // Your data object
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response =await _httpClient.PostAsync("/api/News/GetAll", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            result =await response.Content.ReadAsStringAsync();

            NewsList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result);

            if (!string.IsNullOrEmpty(searchText))
            {
                var news = NewsList.QueryResult.Where(t => t.Title.Contains(searchText) || t.Text.Contains(searchText) || t.Introduction.Contains(searchText)).ToList();
                NewsList.QueryResult = news;
            }

            return Page();
        }
    }
}
