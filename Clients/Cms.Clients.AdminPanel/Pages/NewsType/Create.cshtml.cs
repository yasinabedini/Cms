using Azure;
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cms.Clients.AdminPanel.Pages.NewsType
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public NewsTypeViewModel NewsType { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }
        public void OnGet()
        {
           var data = new { pageNumber = 1, pageSize = 200 };
           var jsonInString = JsonConvert.SerializeObject(data);
           var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
           var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
           var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        }

        public IActionResult OnPost()
        {
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var modelData = new { Title = NewsType.Title, Name = NewsType.Name, LanguageId = NewsType.LanguageId, IsPage = NewsType.IsPage };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/NewsType/Create", modelContent).Result;

            if (methodresponse.IsSuccessStatusCode)
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
