using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class NewsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        private readonly List<NewsViewModel> NewsList;

        public NewsModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async void OnGet(int pageNumber,int typeId,int LanguageId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            
            var data = new { pageNumber = pageNumber, pageSize = 200,typeId= typeId,languageId = LanguageId }; // Your data object
            
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/News/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
        }
    }
}
