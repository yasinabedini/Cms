using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Cms.Endpoints.AdminPanel.Auth;

namespace Cms.Endpoints.AdminPanel.Pages.Sweeper
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<SweeperViewModel> SweeperList { get; set; }

        public ListModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async void OnGet(int pageNumber = 1, int LanguageId = 0)
        {
           _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = pageNumber, pageSize = 300, languageId = LanguageId }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/Sweeper/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            SweeperList = JsonConvert.DeserializeObject<PagedData<SweeperViewModel>>(result);
        }
    }
    #endregion

    #region Create
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public SweeperViewModel Sweeper { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public List<LanguageViewModel> Languages { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }



        public void OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

        }

        public IActionResult OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

            string base64String = Request.Form["Image"];

            if (!ModelState.IsValid)
            {
                if (Image is null)
                {
                    ModelState.AddModelError("Image", "یک عکس برای اسلایدر اپلود کنید");
                }
                return Page();
            }

            #region Save Images
            if (Path.GetExtension(Image.FileName).ToLower() != ".png" && Path.GetExtension(Image.FileName).ToLower() != ".jpg" && Path.GetExtension(Image.FileName).ToLower() != ".jpeg")
            {
                ModelState.AddModelError("Image", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                return Page();
            }

            if (Image.Length > 5000000)
            {
                ModelState.AddModelError("Image", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                return Page();
            }

            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            Image.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "file", Path.GetFileName(Image.FileName));
            var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=sweeper", requestContent).Result;
            Sweeper.ImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            #endregion


            var modelData = new { Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, Image = Sweeper.ImageName, LanguageId = Sweeper.LanguageId };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/Sweeper/Create", modelContent).Result;

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("List");
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
        public SweeperViewModel Sweeper { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public List<LanguageViewModel> Languages { get; set; }

        public void OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            #region Fill Sweeper
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Sweeper/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;
            #endregion

            #region Languages              
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion

            Sweeper = JsonConvert.DeserializeObject<SweeperViewModel>(modelResult);
        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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

            #region Save Image
            if (Image is not null)
            {
                _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={Sweeper.ImageName}&&folder=sweeper");

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                Image.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(Image.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=sweeper", requestContent).Result;
                Sweeper.ImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }
            #endregion

            var modelData = new { Id = Sweeper.Id, Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, ImageName = Sweeper.ImageName, IsEnable = Sweeper.IsEnable, LanguageId = Sweeper.LanguageId };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/Sweeper/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("List");
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

            var result = await _httpClient.DeleteAsync($"/api/Sweeper/Delete?id={id}");
            Console.WriteLine(result.IsSuccessStatusCode);
        }
    }
    #endregion

    #region Change Availibility
    public class ChangeAvailabilityModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;

        [BindProperty]
        public SweeperViewModel Sweeper { get; set; }

        public ChangeAvailabilityModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            #region Fill Sweeper
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/Sweeper/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;

            Sweeper = JsonConvert.DeserializeObject<SweeperViewModel>(modelResult);
            #endregion

            if (Sweeper.IsEnable)
            {
                Sweeper.IsEnable = false;
            }
            else
            {
                Sweeper.IsEnable = true;
            }

            var updateModelData = new { Id = Sweeper.Id, Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, ImageName = Sweeper.ImageName, IsEnable = Sweeper.IsEnable, LanguageId = Sweeper.LanguageId };

            modelJsonInString = JsonConvert.SerializeObject(updateModelData);
            modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            methodresponse = await _httpClient.PutAsync("/api/Sweeper/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("List");
            }
            else
            {
                return Page();
            }
        }
    }
    #endregion
}
