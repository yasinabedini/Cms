using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Cms.Clients.AdminPanel.Pages.Sweeper;

public class ChangeAvailabilityModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _fileManager;

    [BindProperty]
    public SweeperViewModel Sweeper { get; set; }

    public ChangeAvailabilityModel(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("AdminApi");
        _fileManager = factory.CreateClient("FileManager");
    }

    public async Task<IActionResult> OnGet(int id)
    {
        #region Fill Sweeper
        var modelData = new { Id = id };
        var modelJsonInString = JsonConvert.SerializeObject(modelData);
        var modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");
        var methodresponse = _httpClient.PostAsync("/api/Sweeper/GetById", modelContent).Result;
        var modelResult = methodresponse.Content.ReadAsStringAsync().Result;

        Sweeper = JsonConvert.DeserializeObject<SweeperViewModel>(modelResult);
        #endregion

        if (Sweeper.IsEnable)
        {
            Sweeper.IsEnable = false;
        }
        else
        {
            Sweeper.IsEnable = true;
        }

        var updateModelData = new { Id = Sweeper.Id, Title = Sweeper.Title, Text = Sweeper.Text, Link = Sweeper.Link, ImageName = Sweeper.ImageName, IsEnable = Sweeper.IsEnable, LanguageId = Sweeper.LanguageId };

        modelJsonInString = JsonConvert.SerializeObject(updateModelData);
        modelContent = new StringContent(modelJsonInString, Encoding.UTF8, "application/json");

        methodresponse = await _httpClient.PutAsync("/api/Sweeper/Update", modelContent);

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

