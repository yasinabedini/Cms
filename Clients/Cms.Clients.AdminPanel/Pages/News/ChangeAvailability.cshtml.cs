using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class ChangeAvailabilityModel : PageModel
    {
        private readonly HttpClient _httpClient;        

        [BindProperty]
        public NewsViewModel News { get; set; }
        
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
            News = JsonConvert.DeserializeObject<NewsViewModel>(modelResult);

            if (News.IsEnable)
            {
                News.IsEnable = false;
            }
            else
            {
                News.IsEnable = true;
            }

            var updateModelData = new { Id = News.Id, Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, IsEnable = News.IsEnable, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage };

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
