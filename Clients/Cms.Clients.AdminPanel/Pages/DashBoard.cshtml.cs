using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;
using Cms.Clients.AdminPanel.ViewModels;

namespace Cms.Clients.AdminPanel.Pages
{
    public class DashBoardModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<CustomIdentityUser> _userManager;

        private readonly IConfiguration configuration;


        public int UserCount { get; set; }
        public int NewsCount { get; set; }
        public int NotificationNewsCount { get; set; }
        


        public List<CustomIdentityUser> Users { get; set; }

        public DashBoardModel(IHttpClientFactory factory, UserManager<CustomIdentityUser> userManager, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _userManager = userManager;            
            this.configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            UserCount = _userManager.Users.ToList().Count();
            Users = _userManager.Users.ToList().TakeLast(8).ToList();


            var data = new { pageNumber=1, pageSize = 400, typeId = configuration.GetSection("NewsTypeId").Value, isPage = false }; // Your data object
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;
            var result =await response.Content.ReadAsStringAsync();
            NewsCount = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult.Count;

            var secondData = new { pageNumber = 1, pageSize = 400, typeId = configuration.GetSection("NewsNotificationId").Value,  isPage = false }; // Your data object
             jsonInString = JsonConvert.SerializeObject(secondData);
             content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
             response = _httpClient.PostAsync("/api/News/GetAll", content).Result;
             result =await response.Content.ReadAsStringAsync();
            NotificationNewsCount = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult.Count;
        }
    }
}
