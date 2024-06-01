using Cms.Endpoints.AdminPanel.Data;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using Cms.Endpoints.AdminPanel.Pages.NewsType;
using IdentityModel.Client;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.News
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PagedData<NewsViewModel> NewsList { get; set; }
        public List<NewsTypeViewModel> NewsTypes { get; set; }

        public ListModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");

        }

        public async Task<IActionResult> OnGet(int pageNumber = 1, int typeId = 0, int LanguageId = 0, string searchText = "")
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var newsTypeData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(newsTypeData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/NewsType/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            var data = new { pageNumber, pageSize = 200, typeId = typeId, languageId = LanguageId, isPage = false }; // Your data object
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = await _httpClient.PostAsync("/api/News/GetAll", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            result = await response.Content.ReadAsStringAsync();

            NewsList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result);

            if (!string.IsNullOrEmpty(searchText))
            {
                var news = NewsList.QueryResult.Where(t => t.Title.Contains(searchText) || t.Text.Contains(searchText) || t.Introduction.Contains(searchText)).ToList();
                NewsList.QueryResult = news;
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
        public NewsViewModel News { get; set; }

        public List<NewsTypeViewModel> NewsTypes { get; set; }
        public List<LanguageViewModel> Languages { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        [DisplayName("عکس اصلی")]
        public IFormFile Image { get; set; }

        [DisplayName("عکس جانبی")]
        public IFormFile? SecondImage { get; set; }

        [DisplayName("عکس جانبی")]
        public IFormFile? ThirdImage { get; set; }



        public async void OnGet()
        {
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        }

        public async Task<IActionResult> OnPost()
        {
            // _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region Fill Data
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result = await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion

            if (!ModelState.IsValid)
            {
                if (Image is null)
                {
                    ModelState.AddModelError("Image", "یک تصویر برای خبر آپلود کنید!");
                }

                return Page();
            }

            #region Save Main Image

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
            var imageResponse = await _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent);

            News.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

            #endregion

            #region Save Gallery
            if (SecondImage is not null)
            {
                if (Path.GetExtension(SecondImage.FileName).ToLower() != ".png" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpg" && Path.GetExtension(SecondImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("SecondImage", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (SecondImage.Length > 5000000)
                {
                    ModelState.AddModelError("SecondImage", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
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
                News.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

            if (ThirdImage is not null)
            {
                if (Path.GetExtension(ThirdImage.FileName).ToLower() != ".png" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpg" && Path.GetExtension(ThirdImage.FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("ThirdImage", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (ThirdImage.Length > 5000000)
                {
                    ModelState.AddModelError("ThirdImage", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
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
                News.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

            item.Dispose();
            #endregion

            var modelData = new { Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, PublishDate = News.PublishDate, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage, Author = HttpContext.User.GetDisplayName() }; // Your data object

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

        public NewsViewModel News { get; set; }

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
            News = JsonConvert.DeserializeObject<NewsViewModel>(result);

            Author = _userManager.Users.FirstOrDefault(t => t.UserName == News.Author);
        }
    }

    #endregion

    #region Edit

    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public NewsViewModel News { get; set; }

        public List<LanguageViewModel> Languages { get; set; }
        public List<NewsTypeViewModel> NewsTypes { get; set; }

        public IFormFile? Image { get; set; }

        [BindProperty]
        public IFormFile? SecondImage { get; set; }

        [BindProperty]
        public IFormFile? ThirdImage { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public void OnGet(int id)
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region Fill News
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/News/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;
            #endregion

            #region Languages And NewsTypes
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult;

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion

            News = JsonConvert.DeserializeObject<NewsViewModel>(modelResult);
        }

        public async Task<IActionResult> OnPost()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region Languages And NewsTypes
            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult;

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
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
                    ModelState.AddModelError("MainImage", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (Image.Length > 5000000)
                {
                    ModelState.AddModelError("MainImage", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }


                var deleteResult = _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={News.MainImageName}&&folder=news").Result;

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                Image.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(Image.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

                News.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
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

                if (!string.IsNullOrEmpty(News.SecondImage))
                {
                    _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={News.SecondImage}&&folder=news");
                }
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                SecondImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(SecondImage.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                News.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();


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


                if (!string.IsNullOrEmpty(News.ThirdImage))
                {
                    _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={News.ThirdImage}&&folder=news");
                }
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                ThirdImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(ThirdImage.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                News.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

            #endregion


            var modelData = new { Id = News.Id, Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, IsEnable = News.IsEnable, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("Details", new { id = News.Id });
            }
            else
            {
                return Page();
            }
        }
    }
    #endregion
}
