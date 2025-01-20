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
using Cms.Endpoints.AdminPanel.Auth;

namespace Cms.Endpoints.AdminPanel.Pages.News
{
    #region List
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configurationManager;

        public PagedData<NewsViewModel> NewsList { get; set; }
        public List<NewsTypeViewModel> NewsTypes { get; set; }

        public ListModel(IHttpClientFactory factory, IConfiguration configurationManager)
        {
            _httpClient = factory.CreateClient("AdminApi");
            _configurationManager = configurationManager;
        }

        public async Task<IActionResult> OnGet(int pageNumber = 1, int typeId = 0, int LanguageId = 0, string searchText = "",int orderBy=0)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var newsTypeData = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(newsTypeData);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/NewsType/GetAll", content);
            var result = await response.Content.ReadAsStringAsync();
            NewsTypes = JsonConvert.DeserializeObject<PagedData<NewsTypeViewModel>>(result).QueryResult.Where(t => !t.IsPage).ToList();

            var data = new { pageNumber, pageSize = 25, typeId = typeId, languageId = LanguageId, isPage = false }; // Your data object
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
            if (orderBy != 0)
            {
                if (orderBy==1)
                {
                    NewsList.QueryResult = NewsList.QueryResult.OrderBy(t => t.Id).ToList();
                }
                else if (orderBy==2)
                {
                    NewsList.QueryResult = NewsList.QueryResult.OrderBy(t => t.Author).ToList();
                }
                else if (orderBy==3)
                {
                    NewsList.QueryResult = NewsList.QueryResult.OrderBy(t => t.Title).ToList();
                }
                else if (orderBy==4)
                {
                    NewsList.QueryResult = NewsList.QueryResult.OrderBy(t => t.NewsType?.Title).ToList();
                }
            }

            ViewData["disableNews"] = NewsList.QueryResult.Count(t => !t.IsEnable);
            ViewData["newsCount"] = NewsList.QueryResult.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsTypeId").Value));
            ViewData["eventCount"] = NewsList.QueryResult.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsNotificationId").Value));
            ViewData["notificationCount"] = NewsList.QueryResult.Count(t => t.NewsTypeId == int.Parse(_configurationManager.GetSection("NewsId").GetSection("NewsEventId").Value));

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
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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
             _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

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

    #region Gallery & Attachment

    public class GalleryModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<GalleryViewModel> Galleries { get; set; }

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string PageName { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Files { get; set; }
        public GalleryModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }
        public async Task<IActionResult> OnGet(int id, string pageName)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            Id = id;
            PageName = pageName;
            var data = new { id = id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Gallery/GetNewsGallery", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Galleries = JsonConvert.DeserializeObject<List<GalleryViewModel>>(result);


            return Page();
        }

        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> OnPost()
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var data = new { id = Id };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Gallery/GetNewsGallery", content).Result;
            var result = await response.Content.ReadAsStringAsync();
            Galleries = JsonConvert.DeserializeObject<List<GalleryViewModel>>(result);

            #region Save Gallery
            if (Images.Any())
            {
                if (!Galleries.Any(t => !t.Type))
                {
                    var dataModel = new { title = "گالری تصویری ", type = false, newsId = Id };
                    jsonInString = JsonConvert.SerializeObject(dataModel);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/gallery/CreateGallery", content).Result;
                    result = await response.Content.ReadAsStringAsync();

                    var dataValidation = new { id = Id };
                    jsonInString = JsonConvert.SerializeObject(dataValidation);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/Gallery/GetNewsGallery", content).Result;
                    result = await response.Content.ReadAsStringAsync();
                    Galleries = JsonConvert.DeserializeObject<List<GalleryViewModel>>(result);
                }


                var imageGalleryId = Galleries.Where(t => !t.Type).FirstOrDefault().Id;

                foreach (var image in Images)
                {
                    string extension = extension = Path.GetExtension(image.FileName).ToLower();
                    if (extension is not ".png" && extension is not ".jpg" && extension is not ".jpeg")
                    {
                        ModelState.AddModelError("Images", "شما تنها مجاز به آپلود عکس با فرمت های (png - jpg - jpeg) هستید!");
                        return Page();
                    }
                    var requestContent = new MultipartFormDataContent();
                    var item = new MemoryStream();
                    image.CopyTo(item);
                    item.Position = 0;
                    var imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "file", Path.GetFileName(image.FileName));
                    var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=gallery", requestContent).Result;
                    string imageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

                    var dataModel = new { name = imageName, displayName = image.FileName.Replace(extension, ""), galleryId = imageGalleryId, length = image.Length, extension = extension, typeId = 3 };
                    jsonInString = JsonConvert.SerializeObject(dataModel);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/gallery/AddFile", content).Result;
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            #endregion

            #region Save Attachment
            if (Files.Any())
            {
                if (!Galleries.Any(t => t.Type))
                {
                    var dataModel = new { title = "پیوست ها", type = true, newsId = Id };
                    jsonInString = JsonConvert.SerializeObject(dataModel);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/gallery/CreateGallery", content).Result;
                    result = await response.Content.ReadAsStringAsync();

                    var dataValidation = new { id = Id };
                    jsonInString = JsonConvert.SerializeObject(dataValidation);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/Gallery/GetNewsGallery", content).Result;
                    result = await response.Content.ReadAsStringAsync();
                    Galleries = JsonConvert.DeserializeObject<List<GalleryViewModel>>(result);
                }

                var fileGalleryId = Galleries.Where(t => t.Type).FirstOrDefault().Id;

                foreach (var file in Files)
                {
                    string extension = extension = Path.GetExtension(file.FileName).ToLower();
                    int typeId;
                    string type;
                    string mimType;
                    if (extension is ".png" || extension is ".jpg" || extension is ".jpeg")
                    {
                        typeId = 7;
                        type = "image";
                        mimType = "image/jpeg";
                    }
                    else if (extension is ".mp4" || extension is ".mkv")
                    {
                        typeId = 4;
                        type = "video";
                        mimType = "image/jpeg";
                    }
                    else if (extension is ".pdf" || extension is ".zip" || extension is ".rar" || extension is ".txt" || extension is ".docx" || extension is ".pptx" || extension is ".xlsx")
                    {
                        typeId = 5;
                        type = "document";
                        mimType = "application/pdf";
                    }
                    else if (extension is ".mp3" || extension is ".ogg")
                    {
                        typeId = 6;
                        type = "voice";
                        mimType = "image/jpeg";
                    }
                    else
                    {
                        ModelState.AddModelError("Files", "تنها با این فرمت ها میتوانید پیوست آپلود کنید (png-jpg-mp4-mkv-mp3-ogg-pdf-docx-pptx-rar-zip-txt)");
                        return Page();
                    }
                    var requestContent = new MultipartFormDataContent();
                    var item = new MemoryStream();
                    file.CopyTo(item);
                    item.Position = 0;
                    var imageContent = new ByteArrayContent(item.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimType);
                    requestContent.Add(imageContent, "file", Path.GetFileName(file.FileName));
                    var imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=attachment", requestContent).Result;
                    string imageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

                    var dataModel = new { name = imageName, displayName = file.FileName.Replace(extension, ""), galleryId = fileGalleryId, length = file.Length, extension = Path.GetExtension(file.FileName), typeId = typeId };
                    jsonInString = JsonConvert.SerializeObject(dataModel);
                    content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
                    response = _httpClient.PostAsync("/api/gallery/AddFile", content).Result;
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            #endregion

            return RedirectToPage($"gallery", new { id = Id, pageName = HttpContext.Request.Query["pageName"].ToString() });
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

            var result = await _httpClient.DeleteAsync($"/api/News/Delete?id={id}");
            Console.WriteLine(result.IsSuccessStatusCode);
        }
    }
    #endregion

    #region Change Availibility
    public class ChangeAvailibilityModel : PageModel
    {
        private readonly HttpClient _httpClient;
        
        public NewsViewModel News { get; set; }
        

        public ChangeAvailibilityModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task<IActionResult> OnGet(int id,string redirectTo="")
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            #region Fill News
            var modelData = new { Id = id };
            var modelJsonInString = JsonConvert.SerializeObject(modelData);
            var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
            var methodresponse = _httpClient.PostAsync("/api/News/GetById", modelContent).Result;
            var modelResult = methodresponse.Content.ReadAsStringAsync().Result;
            #endregion
            News = JsonConvert.DeserializeObject<NewsViewModel>(modelResult);

            if (News.IsEnable)
            {
                News.IsEnable = false;
            }
            else
            {
                News.IsEnable = true;
            }

            var updateModelData = new { Id = News.Id, Title = News.Title, Introduction = News.Introduction, LanguageId = News.LanguageId, NewsTypeId = News.NewsTypeId, IsEnable = News.IsEnable, Text = News.Text, MainImage = News.MainImageName, SecondImage = News.SecondImage, ThirdImage = News.ThirdImage };

            modelJsonInString = JsonConvert.SerializeObject(updateModelData);
            modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

            methodresponse = await _httpClient.PutAsync("/api/News/Update", modelContent);

            if (methodresponse.IsSuccessStatusCode)
            {
                return LocalRedirect(redirectTo);
            }
            else
            {
                return Page();
            }
        }

    }
    #endregion

    #region Delete File From Gallery
    public class DeleteFileModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteFileModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task<IActionResult> OnGet(string fileName, string pageAddress, int newsId)
        {
            _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result);

            var result = await _httpClient.DeleteAsync($"/api/Gallery/DeleteFile?fileName={fileName}");
            return RedirectToPage("Gallery", new { id = newsId, pageName = pageAddress });
        }
    }
    #endregion
}
