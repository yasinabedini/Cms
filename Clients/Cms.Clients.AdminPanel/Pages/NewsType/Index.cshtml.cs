using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;

namespace Cms.Clients.AdminPanel.Pages.NewsType
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<NewsTypeViewModel> NewsTypeList { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public IActionResult OnGet()
        {
            var data = new { pageNumber = 1, pageSize = 200 }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            NewsTypeList = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t=>!t.IsPage).ToList();

            return Page();
        }
    }
}
