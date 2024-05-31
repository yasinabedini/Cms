using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.Language;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.Sweeper
{
    public class SweeperListModel:PageModel
    {
    }

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
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

            var data = new { pageNumber = 1, pageSize = 200 };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/Language/GetAll", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Languages = JsonConvert.DeserializeObject<PagedData<LanguageViewModel>>(result).QueryResult;

        }

        public IActionResult OnPost()
        {
            //_httpClient.SetBearerToken(Token.GetTokenResponse(_httpClient, HttpContext).Result.AccessToken);

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
}
