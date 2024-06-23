using Cms.Endpoints.AdminPanel.Auth;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.Setting
{
    #region Info

    #region Get All Info
    public class GetAllInfoModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<InfoViewModel> InfoList { get; set; }

        public GetAllInfoModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var newsTypeData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(newsTypeData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Info/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            InfoList = JsonConvert.DeserializeObject<PagedData<InfoViewModel>>(result);
        }
    }
    #endregion

    public class DeleteInfoModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteInfoModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }


        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var result = await _httpClient.DeleteAsync($"/api/Info/Delete?id={id}");            
        }
    }

    #region Update Info
    public class UpdateInfoModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public InfoViewModel Info { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public UpdateInfoModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            var infodata = new { id = id };
            jsonInString = JsonConvert.SerializeObject(infodata);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Info/GetById", content).Result;
            result = await response.Content.ReadAsStringAsync();
            Info = JsonConvert.DeserializeObject<InfoViewModel>(result);
        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var Data = new { id = Info.Id, address = Info.Address, workTime = Info.WorkTime, phoneNumber = Info.PhoneNumber, eitaaAddress = Info.EitaaAddress, emailAddress = Info.EmailAddress, instagramAddress = Info.InstagramAddress, languageId = Info.LanguageId };
            var jsonInString = JsonConvert.SerializeObject(Data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Info/UpdateInfo", content);

            return RedirectToPage("infoList");
        }
    }
    #endregion

    #region Create Info
    public class CreateInfoModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public InfoViewModel Info { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public CreateInfoModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var Data = new { address = Info.Address, workTime = Info.WorkTime, phoneNumber = Info.PhoneNumber, eitaaAddress = Info.EitaaAddress, emailAddress = Info.EmailAddress, instagramAddress = Info.InstagramAddress, languageId = Info.LanguageId };
            var jsonInString = JsonConvert.SerializeObject(Data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Info/Create", content);

            return RedirectToPage("infoList");
        }
    }
    #endregion

    #endregion


    #region Link

    #region Create Link
    public class AddLinkModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public LinkViewModel Link { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public AddLinkModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            if (!ModelState.IsValid)
            {
                var data = new { pageNumber = 1, pageSize = 200 };
                var lanJsonInString = JsonConvert.SerializeObject(data);
                var lanContent = new StringContent(lanJsonInString, Encoding.UTF8, "application/json");
                var lanResponse = _httpClient.PostAsync("/api/Language/GetAll", lanContent).Result;
                var result = await lanResponse.Content.ReadAsStringAsync();
                Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
                return Page();
            }

            var Data = new { link = Link.Link, title = Link.Title, languageId = Link.LanguageId };
            var jsonInString = JsonConvert.SerializeObject(Data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Info/AddLink", content);

            return RedirectToPage("Links");
        }
    }
    #endregion

    public class DeleteLinkModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteLinkModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }


        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var result = await _httpClient.DeleteAsync($"/api/Info/DeleteLink?id={id}");            
        }
    }

    #region Update Link
    public class UpdateLinkModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public LinkViewModel Link { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public UpdateLinkModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            var infodata = new { id = id };
            jsonInString = JsonConvert.SerializeObject(infodata);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Info/GetLinkById", content).Result;
            result = await response.Content.ReadAsStringAsync();
            Link = JsonConvert.DeserializeObject<LinkViewModel>(result);
        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var Data = new { id = Link.Id, link = Link.Link, title = Link.Title, languageId = Link.LanguageId };
            var jsonInString = JsonConvert.SerializeObject(Data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Info/UpdateLink", content);

            return RedirectToPage("Links");
        }
    }
    #endregion

    #region Get All Links
    public class LinksModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<LinkViewModel> Links { get; set; }

        public LinksModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Info/GetAllLinks", content);
            var result = await response.Content.ReadAsStringAsync();
            Links = JsonConvert.DeserializeObject<List<LinkViewModel>>(result);
        }
    }
    #endregion 

    #endregion
}
