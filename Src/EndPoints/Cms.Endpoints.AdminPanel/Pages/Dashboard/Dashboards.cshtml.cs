using System;
using System.Collections.Generic;
using System.Text;
using Cms.Endpoints.AdminPanel.Data;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.News;
using Cms.Endpoints.AdminPanel.Pages.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Cms.Endpoints.AdminPanel.Pages.Dashboard
{
    public class AnalyticsModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configurationManager;


        public AnalyticsModel(UserManager<CustomIdentityUser> userManager, IHttpClientFactory factory, IConfiguration configurationManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _httpClient = factory.CreateClient("AdminApi");
            _configurationManager = configurationManager;
            _roleManager = roleManager;
        }


        public List<UserViewModel> Users { get; set; }
        public List<NewsViewModel> News { get; set; }


        public async Task OnGet()
        {
            Users = _userManager.Users.Select(t => new UserViewModel
            {
                UserName = t.UserName,
                PhoneNumber = t.PhoneNumber,
                Name = t.Name,
                Email = t.Email,
                Id = t.Id,
                LockoutEnabled = t.LockoutEnabled                
            }).ToList();

            var data = new { pageNumber = 1, pageSize = 25, typeId = 0, languageId = 0, isPage = false }; // Your data object
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/News/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            News = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;



            ViewData["newsCount"] = News.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsTypeId").Value));
            ViewData["eventCount"] = News.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsEventId").Value));
            ViewData["notificationCount"] = News.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsNotificationId").Value));

            ViewData["roleCount"] = _roleManager.Roles.Count();

        }
    }
    public class CRMModel : PageModel
    {
        public void OnGet() { }
    }
    public class EcommerceModel : PageModel
    {
        public void OnGet() { }
    }
}
