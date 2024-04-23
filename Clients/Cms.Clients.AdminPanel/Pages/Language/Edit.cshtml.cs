using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Language
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public LanguageViewModel Language { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public void OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Language/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;

            Language = JsonConvert.DeserializeObject<LanguageViewModel>(modelResult);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);
            var defaultModelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(defaultModelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Language/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;

            if (!ModelState.IsValid)
            {

                return Page();
            }

            var modelData = new {Id=Language.Id, Title = Language.Title, Name = Language.Name, Rtl = Language.Rtl, Region = Language.Region,IsEnable=Language.IsEnable };

            modelJsonInString = JsonConvert.SerializeObject(modelData);
            modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            methodresponse = await _httpClient.PutAsync("/api/Language/Update", modelContent);

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
