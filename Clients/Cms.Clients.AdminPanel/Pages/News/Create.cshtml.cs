
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using IdentityModel.Client;
using Cms.Clients.AdminPanel.Auth;
using IdentityServer4.Extensions;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;

        [BindProperty]
        public NewsViewModel News { get; set; }

        public CreateModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");
        }


        public List<NewsTypeViewModel> NewsTypes { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
        [BindProperty]
        public IFormFile MainImage { get; set; }

        [BindProperty]
        public List<IFormFile>? Images { get; set; }
      
        public async void OnGet()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/NewsType/GetAll", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult;

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result =await response.Content.ReadAsStringAsync();
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
            var result =await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult;

            data = new { pageNumber = 1, pageSize = 200 };
            jsonInString = JsonConvert.SerializeObject(data);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            result =await response.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
            #endregion
            
            if (!ModelState.IsValid)
            {
                if (MainImage is null)
                {
                    ModelState.AddModelError("MainImage", "یک عکس برای خبر اپلود کنید");
                }

                return Page();
            }

            #region Save Main Image

            if (Path.GetExtension(MainImage.FileName).ToLower() != ".png" && Path.GetExtension(MainImage.FileName).ToLower() != ".jpg" && Path.GetExtension(MainImage.FileName).ToLower() != ".jpeg")
            {
                ModelState.AddModelError("MainImage", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                return Page();
            }

            if (MainImage.Length > 5000000)
            {
                ModelState.AddModelError("MainImage", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                return Page();
            }
            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            MainImage.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", Path.GetFileName(MainImage.FileName));
            var imageResponse = await _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent);

            News.MainImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();

            #endregion

            #region Save Gallery
            if (Images is not null && Images.Count is not 0)
            {
                if (Path.GetExtension(Images[0].FileName).ToLower() != ".png" && Path.GetExtension(Images[0].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[0].FileName).ToLower() != ".jpeg")
                {
                    ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                    return Page();
                }

                if (Images[0].Length > 5000000)
                {
                    ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                    return Page();
                }

                requestContent = new MultipartFormDataContent();
                item = new MemoryStream();
                Images[0].CopyTo(item);
                item.Position = 0;
                imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "image", Path.GetFileName(Images[0].FileName));
                imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                News.SecondImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();

                if (Images.Count==2)
                {
                    if (Path.GetExtension(Images[1].FileName).ToLower() != ".png" && Path.GetExtension(Images[1].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[1].FileName).ToLower() != ".jpeg")
                    {
                        ModelState.AddModelError("Images", "شما فقط مجاز به اپلود عکس با این فرمت ها هستید. (png-jpg)");
                        return Page();
                    }

                    if (Images[1].Length > 5000000)
                    {
                        ModelState.AddModelError("Images", "حداکثر حجم عکس آپلود شده باید 5 مگابایت باشد!");
                        return Page();
                    }

                    requestContent = new MultipartFormDataContent();
                    item = new MemoryStream();
                    Images[1].CopyTo(item);
                    item.Position = 0;
                    imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "image", Path.GetFileName(Images[1].FileName));
                    imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                    News.ThirdImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
                }
            }
            item.Dispose();
            #endregion

            var modelData = new { Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, PublishDate = News.PublishDate, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage, Author = HttpContext.User.GetDisplayName() }; // Your data object

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse =await _httpClient.PostAsync("/api/News/Create", modelContent);

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
