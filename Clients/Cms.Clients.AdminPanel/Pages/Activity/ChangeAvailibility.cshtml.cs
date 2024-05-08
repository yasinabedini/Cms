using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Activity
{
    public class ChangeAvailabilityModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public NewsViewModel Activity { get; set; }

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
            Activity = JsonConvert.DeserializeObject<NewsViewModel>(modelResult);

            if (Activity.IsEnable)
            {
                Activity.IsEnable = false;
            }
            else
            {
                Activity.IsEnable = true;
            }

            var updateModelData = new { Id = Activity.Id, Title = Activity.Title, Introduction = Activity.Introduction, LanguageId = Activity.LanguageId, NewsTypeId = Activity.NewsTypeId, IsEnable = Activity.IsEnable, Text = Activity.Text, MainImage = Activity.MainImageName, SecondImage = Activity.SecondImage, ThirdImage = Activity.ThirdImage };

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
