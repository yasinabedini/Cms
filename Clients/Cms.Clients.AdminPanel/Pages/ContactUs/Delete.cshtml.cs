using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.ContactUs
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public DeleteModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("AdminApi");
        }


        public async Task OnGet(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Contact/Delete?id={id}");
            Console.WriteLine(result.IsSuccessStatusCode);
        }
    }
}
