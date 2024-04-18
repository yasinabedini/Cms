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

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public async Task<IActionResult> OnGet(int pageNumber = 1, int typeId = 0, int LanguageId = 0)
        {            
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber, pageSize = 200, typeId, languageId = LanguageId, isPage = false }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            NewsList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result);

            return Page();
        }
    }
}
