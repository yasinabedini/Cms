using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;

        [BindProperty]
        public NewsViewModel News { get; set; }

        public List<LanguageViewModel> Languages { get; set; }
        public List<NewsTypeViewModel> NewsTypes { get; set; }

        public IFormFile? MainImage { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _fileManager = factory.CreateClient("FileManager");
        }

        public void OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

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

        public async Task<IActionResult> OnPost(List<IFormFile>? images)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            if (!ModelState.IsValid)
            {

                return Page();
            }

            #region Update Gallery
            if (MainImage is not null)
            {
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


                var deleteResult = _fileManager.DeleteAsync($"/api/FileManager/Delete?imageName={News.MainImageName}&&folder=news").Result;

                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                MainImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "image", Path.GetFileName(MainImage.FileName));
                var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

                News.MainImageName = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
            }

            if (images is not null && images.Count is not 0)
            {
                if (!string.IsNullOrEmpty(News.SecondImage))
                {
                    _fileManager.DeleteAsync($"/api/FileManager/Delete?imageName={News.SecondImage}&&folder=news");
                }
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                images[0].CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "image", Path.GetFileName(images[0].FileName));
                var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                News.SecondImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();

                if (images[1] is not null)
                {
                    if (!string.IsNullOrEmpty(News.ThirdImage))
                    {
                        _fileManager.DeleteAsync($"/api/FileManager/Delete?imageName={News.ThirdImage}&&folder=news");
                    }

                    requestContent = new MultipartFormDataContent();
                    item = new MemoryStream();
                    images[1].CopyTo(item);
                    item.Position = 0;
                    imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "image", Path.GetFileName(images[1].FileName));
                    imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                    News.SecondImage = imageResponse.Headers.First(t => t.Key == "imageName").Value.First();
                }

                item.Dispose();
            }
            #endregion


            var modelData = new { Id = News.Id, Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, IsEnable = News.IsEnable, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage };

            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            var methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

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
