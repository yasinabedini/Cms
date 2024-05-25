using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.AboutUs
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;        


        [BindProperty]
        public NewsViewModel About { get; set; }
        public List<NewsViewModel> AboutList { get; set; }
        public List<LanguageViewModel> Languages { get; set; }

        [BindProperty]
        public IFormFile? MainImage { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public EditModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");            

        }

        public async Task<IActionResult> OnGet(int id)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult =await languageResponse.Content.ReadAsStringAsync();
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            #region Activity List
            var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response =await _httpClient.PostAsync("/api/News/GetAll", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result =await response.Content.ReadAsStringAsync();

            AboutList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;
            #endregion

            var silngeData = new { Id = id }; // Your data object

            jsonInString = JsonConvert.SerializeObject(silngeData);
            content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            response =await _httpClient.PostAsync("/api/News/GetById", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            result =await response.Content.ReadAsStringAsync();

            About = JsonConvert.DeserializeObject<NewsViewModel>(result);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            #region languages
            var languageData = new { pageNumber = 1, pageSize = 200 };
            var languageJsonInString = JsonConvert.SerializeObject(languageData);
            var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
            var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
            var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
            #endregion

            #region Activity List
            var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            var result = response.Content.ReadAsStringAsync().Result;

            AboutList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;
            #endregion

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

                var deleteResult = _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={About.MainImageName}&&folder=news").Result;
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                MainImage.CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(MainImage.FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;

                About.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }

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

                if (!string.IsNullOrEmpty(About.SecondImage))
                {
                    _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={About.SecondImage}&&folder=news");
                }
                var requestContent = new MultipartFormDataContent();
                var item = new MemoryStream();
                Images[0].CopyTo(item);
                item.Position = 0;
                var imageContent = new ByteArrayContent(item.ToArray());
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "file", Path.GetFileName(Images[0].FileName));
                var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                About.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

                if (Images.Count == 2)
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


                    if (!string.IsNullOrEmpty(About.ThirdImage))
                    {
                        _httpClient.DeleteAsync($"/api/FileManager/Delete?imageName={About.ThirdImage}&&folder=news");
                    }

                    requestContent = new MultipartFormDataContent();
                    item = new MemoryStream();
                    Images[1].CopyTo(item);
                    item.Position = 0;
                    imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "file", Path.GetFileName(Images[1].FileName));
                    imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                    About.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
                }

                item.Dispose();
            }
            #endregion

            var modelData = new { Id = About.Id, Title = About.Title, Introduction = About.Introduction, LanguageId = About.LanguageId, NewsTypeId = About.NewsTypeId, IsEnable = About.IsEnable, PublishDate = About.PublishDate, Text = About.Text, MainImage = About.MainImageName, SecondImage = About.SecondImage, ThirdImage = About.ThirdImage };

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
