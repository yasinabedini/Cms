using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.ContactUs
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<ContactViewModel> ContactList { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async void OnGet(int pageNumber = 1)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new {pageNumber =  pageNumber, pageSize = 200}; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/Contact/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            ContactList = JsonConvert.DeserializeObject<PagedData<ContactViewModel>>(result);
        }
    }
}
