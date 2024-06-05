using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.NewsType
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<NewsTypeViewModel> NewsTypeList { get; set; }

        public ListModel(IHttpClientFactory factory)
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

            NewsTypeList = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            return Page();
        }
    }
    #endregion

    #region Create
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

            var modelData = new { Title = NewsType.Title, Name = NewsType.Name, LanguageId = NewsType.LanguageId, IsPage = false };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/NewsType/Create", modelContent).Result;

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("list");
            }
            else
            {
                return Page();
            }
        }
    }
    #endregion

    #region Edit
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
            var data = new { id = id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetById", content).Result;
            var result = await response.Content.ReadAsStringAsync();
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


            var modelData = new { Id = NewsType.Id, Title = NewsType.Title, Name = NewsType.Name, LanguageId = NewsType.LanguageId, IsPage = false, IsEnable = NewsType.IsEnable };

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
    #endregion

    #region Delete
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }


        public async Task OnGet(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/NewsType/Delete?id={id}");
            Console.WriteLine(result.IsSuccessStatusCode);
        }
    }
    #endregion
}
