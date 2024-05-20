using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.News
{
    public class DeleteFileModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteFileModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task<IActionResult> OnGet(string fileName,string pageAddress,int newsId)
        {
            var result = await _httpClient.DeleteAsync($"/api/Gallery/DeleteFile?fileName={fileName}");
            return RedirectToPage("Gallery", new { id = newsId, pageName = pageAddress });
        }
    }
}
