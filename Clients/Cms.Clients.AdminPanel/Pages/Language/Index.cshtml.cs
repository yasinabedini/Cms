using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Cms.Clients.AdminPanel.Auth;

namespace Cms.Clients.AdminPanel.Pages.Language
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public List<LanguageViewModel> Languages { get; set; }
        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public void OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber = 1, pageSize = 200, }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        }
    }
}
