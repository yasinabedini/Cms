using Cms.Clients.AdminPanel.Data;
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class DetailsModel : PageModel
    {        
        private readonly HttpClient _httpClient;

        public NewsViewModel News { get; set; }
        

        public DetailsModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");            
        }

        public async void OnGet(int id)
        {
            var data = new { id = id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/News/GetById", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            News = JsonConvert.DeserializeObject<NewsViewModel>(result);            
        }
    }
}
