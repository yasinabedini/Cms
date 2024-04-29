using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.NewsType
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public NewsTypeViewModel NewsType { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public async Task<IActionResult> OnGet(int id)
        {
            var data = new { id=id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetById", content).Result;
            var result =await response.Content.ReadAsStringAsync();
            NewsType = JsonConvert.DeserializeObject<NewsTypeViewModel>(result);

             var languageData = new { pageNumber = 1, pageSize = 200 };
             jsonInString = JsonConvert.SerializeObject(languageData);
             content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
             response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
             result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(languageData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            if (!ModelState.IsValid)
            {
                return Page();
            }


            var modelData = new { Id = NewsType.Id, Title = NewsType.Title, Name = NewsType.Name, LanguageId = NewsType.LanguageId, IsPage = NewsType.IsPage, IsEnable = NewsType.IsEnable};

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/NewsType/Update", modelContent);

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
