using Azure;
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class GalleryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _fileManager;

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
            _fileManager = factory.CreateClient("FileManager");
        }
        public async Task<IActionResult> OnGet(int id, string pageName)
        {
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

        public async Task<IActionResult> OnPost()
        {
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
                    var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=gallery", requestContent).Result;
                    string imageName = imageResponse.Headers.First(t => t.Key == "fileName").Value.First();

                    var dataModel = new { name = imageName, displayName = image.FileName.Replace(extension,""), galleryId = imageGalleryId, length = image.Length, extension = extension, typeId = 3 };
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
                    var imageResponse = _fileManager.PostAsync($"/api/FileManager/upload?folder=attachment", requestContent).Result;
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
}
