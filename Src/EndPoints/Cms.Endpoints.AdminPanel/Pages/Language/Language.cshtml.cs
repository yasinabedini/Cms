using Cms.Endpoints.AdminPanel.Pages.Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Cms.Endpoints.AdminPanel.Auth;

namespace Cms.Endpoints.AdminPanel.Pages.Language
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public List<LanguageViewModel> Languages { get; set; }
        public ListModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async void OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200, }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();

            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        }
    }
    #endregion

    #region Create
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public LanguageViewModel Language { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
             _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var modelData = new { Title = Language.Title, Name = Language.Name, Rtl = Language.Rtl, Region = Language.Region };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PostAsync("/api/Language/Create", modelContent);

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
        public LanguageViewModel Language { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse =await _httpClient.PostAsync("/api/Language/GetById", modelContent);
            var modelResult =await methodresponse.Content.ReadAsStringAsync();

            Language = JsonConvert.DeserializeObject<LanguageViewModel>(modelResult);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var defaultModelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(defaultModelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Language/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;

            if (!ModelState.IsValid)
            {

                return Page();
            }

            var modelData = new { Id = Language.Id, Title = Language.Title, Name = Language.Name, Rtl = Language.Rtl, Region = Language.Region, IsEnable = Language.IsEnable };

            modelJsonInString = JsonConvert.SerializeObject(modelData);
            modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            methodresponse = await _httpClient.PutAsync("/api/Language/Update", modelContent);

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
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var result = await _httpClient.DeleteAsync($"/api/Language/Delete?id={id}");
            Console.WriteLine(result.IsSuccessStatusCode);
        }
    }
    #endregion
}
