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

        public void OnGet()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken); 

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


        }

        public IActionResult OnPost(IFormFile mainImage, List<IFormFile> images)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            if (!ModelState.IsValid)
            {
                if (mainImage is null)
                {
                    ModelState.AddModelError("mainImage", "یک عکس برای خبر اپلود کنید");
                }

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

                return Page();
            }

            #region Save Main Image
            var requestContent = new MultipartFormDataContent();
            var item = new MemoryStream();
            mainImage.CopyTo(item);
            item.Position = 0;
            var imageContent = new ByteArrayContent(item.ToArray());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", Path.GetFileName(mainImage.FileName));
            var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

            News.MainImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();

            #endregion

            #region Save Gallery
            if (images is not null && images.Count is not 0)
            {
                requestContent = new MultipartFormDataContent();
                item = new MemoryStream();
                images[0].CopyTo(item);
                item.Position = 0;
                imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "image", Path.GetFileName(images[0].FileName));
                imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                News.SecondImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();

                if (images[1] is not null)
                {
                    requestContent = new MultipartFormDataContent();
                    item = new MemoryStream();
                    images[1].CopyTo(item);
                    item.Position = 0;
                    imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "image", Path.GetFileName(images[1].FileName));
                    imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                    News.ThirdImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
                }
            }
            item.Dispose();
            #endregion

            var modelData = new { Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, PublishDate = News.PublishDate, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage }; // Your data object

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = _httpClient.PostAsync("/api/News/Create", modelContent).Result;

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
