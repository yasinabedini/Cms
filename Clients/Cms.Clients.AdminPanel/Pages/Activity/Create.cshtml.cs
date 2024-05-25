using Cms.Clients.AdminPanel.Auth;
using Cms.Clients.AdminPanel.ViewModels;
using IdentityModel.Client;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Activity;

public class CreateModel : PageModel
{
    private readonly HttpClient _httpClient;    


    [BindProperty]
    public NewsViewModel Activity { get; set; }
    public List<NewsViewModel> ActivityList { get; set; }
    public List<LanguageViewModel> Languages { get; set; }

    [BindProperty]
    public IFormFile Image { get; set; }

    [BindProperty]
    public List<IFormFile>? Images { get; set; }


    public CreateModel(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("AdminApi");        

    }
    public IActionResult OnGet()
    {

        _httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

        IConfiguration config;

        config = HttpContext.RequestServices.GetRequiredService(typeof(IConfiguration)) as IConfiguration;

        ViewData["ActivityId"] = config.GetSection("NewsId")["NewsActivityId"];

        #region languages
        var languageData = new { pageNumber = 1, pageSize = 200 };
        var languageJsonInString = JsonConvert.SerializeObject(languageData);
        var languageContent = new StringContent(languageJsonInString, Encoding.UTF8, "application/json");
        var languageResponse = _httpClient.PostAsync("/api/Language/GetAll", languageContent).Result;
        var languageResult = languageResponse.Content.ReadAsStringAsync().Result;
        Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(languageResult).QueryResult;
        #endregion

        var data = new { pageNumber = 1, pageSize = 200, typeId = 7, isPage = true }; // Your data object

        var jsonInString = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");

        var response = _httpClient.PostAsync("/api/News/GetAll", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest();
        }
        var result = response.Content.ReadAsStringAsync().Result;

        ActivityList = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

        #region languages
        var data = new { pageNumber = 1, pageSize = 200 };
        var jsonInString = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
        var response =await _httpClient.PostAsync("/api/Language/GetAll", content);
        var result =await response.Content.ReadAsStringAsync();
        Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;
        #endregion

        if (!ModelState.IsValid)
        
        {
            if (Image is null)
            {
                ModelState.AddModelError("Image", "یک عکس برای فعالیت اپلود کنید");
            }
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
        var imageResponse =await _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent);

        Activity.MainImageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
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
            requestContent.Add(imageContent, "file", Path.GetFileName(Images[0].FileName));
            imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
            Activity.SecondImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

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
                requestContent.Add(imageContent, "file", Path.GetFileName(Images[1].FileName));
                imageResponse = _httpClient.PostAsync($"/api/FileManager/upload?folder=news", requestContent).Result;
                Activity.ThirdImage = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();
            }
        }
        item.Dispose();
        #endregion

        var modelData = new { Title = Activity.Title, Introduction = Activity.Introduction, LanguageId = Activity.LanguageId, NewsTypeId = Activity.NewsTypeId, PublishDate = Activity.PublishDate, Text = Activity.Text, MainImage = Activity.MainImageName, SecondImage = Activity.SecondImage, ThirdImage = Activity.ThirdImage, Author = HttpContext.User.GetDisplayName() }; // Your data object

        var modelJsonInString = JsonConvert.SerializeObject(modelData);
        var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

        var methodresponse = await _httpClient.PostAsync("/api/News/Create", modelContent);

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