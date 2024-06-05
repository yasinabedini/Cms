using Cms.Endpoints.AdminPanel.Data;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using Cms.Endpoints.AdminPanel.Pages.News;
using IdentityModel.Client;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.Activity
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<NewsViewModel> ActivityList { get; set; }
        public List<LanguageViewModel> Languages { get; set; }


        public ListModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public IActionResult OnGet(string searchText = "", int orderBy = 0)
        {
            // _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);
            var config = (IConfiguration)HttpContext.RequestServices.GetRequiredService(typeof(IConfiguration));

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion


            var data = new { pageNumber = 1, pageSize = 200, typeId = config.GetSection("NewsId").GetSection("NewsActivityId").Value, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            ActivityList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

            if (!string.IsNullOrEmpty(searchText))
            {
                ActivityList = ActivityList.Where(t => t.Title.Contains(searchText) || t.Text.Contains(searchText)).ToList();
            }
            if (orderBy != 0)
            {
                ActivityList = ActivityList.OrderBy(t => t.Title).ToList();
            }

            return Page();
        }
    }
    #endregion

    #region Create
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;


        [BindProperty]
        public NewsViewModel Activity { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "یک عکس برای فعالیت اپلود کنید")]
        public IFormFile Image { get; set; }

        [BindProperty]
        public IFormFile? SecondImage { get; set; }

        [BindProperty]
        public IFormFile? ThirdImage { get; set; }


        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }
        public async Task<IActionResult> OnGet()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            IConfiguration config;

            config = HttpContext.RequestServices.GetRequiredService(typeof(IConfiguration)) as IConfiguration;

            ViewData["ActivityId"] = config.GetSection("NewsId")["NewsActivityId"];

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = await _httpClient.PostAsync("/api/Language/GetAll", languageContent);
            var languageResult = await languageResponse.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Language/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion

            if (!ModelState.IsValid)
            {
                return Page();
            }


            #region Save Main Image

            if (Path.GetExtension(Image.FileName).ToLower() != ".png" && Path.GetExtension(Image.FileName).ToLower() != ".jpg" && Path.GetExtension(Image.FileName).ToLower() != ".jpeg")
            {
                ModelState.AddModelError("Image", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                return Page();
            }

            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            Image.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "file", Path.GetFileName(Image.FileName));
            var imageResponse = await _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent);

            Activity.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            #endregion

            #region Save Gallery
            if (SecondImage is not null)
            {
                if (Path.GetExtension(SecondImage.FileName).ToLower() != ".png" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpg" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (SecondImage.Length > 5000000)
                {
                    ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }

                requestContent = new MultipartFormDataContent();
                item = new MemoryStream();
                SecondImage.CopyTo(item);
                item.Position = 0;
                imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(SecondImage.FileName));
                imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                Activity.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

            if (ThirdImage is not null)
            {
                if (Path.GetExtension(ThirdImage.FileName).ToLower() != ".png" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpg" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (ThirdImage.Length > 5000000)
                {
                    ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }

                requestContent = new MultipartFormDataContent();
                item = new MemoryStream();
                ThirdImage.CopyTo(item);
                item.Position = 0;
                imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(ThirdImage.FileName));
                imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                Activity.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

            item.Dispose();
            #endregion

            var modelData = new { Title = Activity.Title, Introduction = Activity.Introduction, LanguageId = Activity.LanguageId, NewsTypeId = Activity.NewsTypeId, PublishDate = Activity.PublishDate, Text = Activity.Text, MainImage = Activity.MainImageName, SecondImage = Activity.SecondImage, ThirdImage = Activity.ThirdImage, Author = HttpContext.User.GetDisplayName() }; // Your data object

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PostAsync("/api/News/Create", modelContent);

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

    #region Details
    public class DetailsModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;

        private readonly HttpClient _httpClient;

        public NewsViewModel Activity { get; set; }

        public CustomIdentityUser Author { get; set; }

        public DetailsModel(IHttpClientFactory factory, UserManager<CustomIdentityUser> userManager)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _userManager = userManager;
        }

        public async void OnGet(int id)
        {
            var data = new { id = id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/News/GetById", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Activity = JsonConvert.DeserializeObject<NewsViewModel>(result);

            Author = _userManager.Users.FirstOrDefault(t => t.UserName == Activity.Author);
        }
    }
    #endregion

    #region Edit

    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;


        [BindProperty]
        public NewsViewModel Activity { get; set; }

        public List<LanguageViewModel> Languages { get; set; }
        public IFormFile? Image { get; set; }

        [BindProperty]
        public IFormFile? SecondImage { get; set; }

        [BindProperty]
        public IFormFile? ThirdImage { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public IActionResult OnGet(int id)
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion


            var data = new { Id = id }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetById", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            Activity = JsonConvert.DeserializeObject<NewsViewModel>(result);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region Update Gallery
            if (Image is not null)
            {
                if (Path.GetExtension(Image.FileName).ToLower() != ".png" && Path.GetExtension(Image.FileName).ToLower() != ".jpg" && Path.GetExtension(Image.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Image", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (Image.Length > 5000000)
                {
                    ModelState.AddModelError("MainImage", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }

                var deleteResult = _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={Activity.MainImageName}&&folder=news").Result;

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                Image.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(Image.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

                Activity.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }


            if (SecondImage is not null)
            {
                if (Path.GetExtension(SecondImage.FileName).ToLower() != ".png" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpg" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }
                if (SecondImage.Length > 5000000)
                {
                    ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }

                if (!string.IsNullOrEmpty(Activity.SecondImage))
                {
                    _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={Activity.SecondImage}&&folder=news");
                }
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                SecondImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(SecondImage.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                Activity.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

                item.Dispose();
            }

            if (ThirdImage is not null)
            {
                if (Path.GetExtension(ThirdImage.FileName).ToLower() != ".png" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpg" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }
                if (ThirdImage.Length > 5000000)
                {
                    ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }


                if (!string.IsNullOrEmpty(Activity.ThirdImage))
                {
                    _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={Activity.ThirdImage}&&folder=news");
                }

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                ThirdImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(ThirdImage.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                Activity.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }
            #endregion

            var modelData = new { Id = Activity.Id, Title = Activity.Title, Introduction = Activity.Introduction, LanguageId = Activity.LanguageId, NewsTypeId = Activity.NewsTypeId, IsEnable = Activity.IsEnable, PublishDate = Activity.PublishDate, Text = Activity.Text, MainImage = Activity.MainImageName, SecondImage = Activity.SecondImage, ThirdImage = Activity.ThirdImage };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("Details", new { id = Activity.Id });
            }
            else
            {
                return Page();
            }
        }
    }

    #endregion
}
