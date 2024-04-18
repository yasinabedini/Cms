using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Sweeper
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<SweeperViewModel> SweeperList { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async void OnGet(int pageNumber = 1, int typeId = 0, int LanguageId = 0)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber, pageSize = 200, typeId, languageId = LanguageId, isPage = false }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/Sweeper/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            SweeperList = JsonConvert.DeserializeObject<PagedData<SweeperViewModel>>(result);
        }
    }
}
