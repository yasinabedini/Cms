using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Asnad
{
    public class ChangeAvailabilityModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public NewsViewModel Asnad { get; set; }

        public IFormFile? MainImage { get; set; }

        public ChangeAvailabilityModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            #region Fill News
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/News/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;
            #endregion
            Asnad = JsonConvert.DeserializeObject<NewsViewModel>(modelResult);

            if (Asnad.IsEnable)
            {
                Asnad.IsEnable = false;
            }
            else
            {
                Asnad.IsEnable = true;
            }

            var updateModelData = new { Id = Asnad.Id, Title = Asnad.Title, Introduction = Asnad.Introduction, LanguageId = Asnad.LanguageId, NewsTypeId = Asnad.NewsTypeId, IsEnable = Asnad.IsEnable, Text = Asnad.Text, MainImage = Asnad.MainImageName, SecondImage = Asnad.SecondImage, ThirdImage = Asnad.ThirdImage };

            modelJsonInString = JsonConvert.SerializeObject(updateModelData);
            modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

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
