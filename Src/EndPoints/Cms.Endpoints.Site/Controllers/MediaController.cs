using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Cms.Endpoints.Site.Proxy.Asnad;
using Cms.Endpoints.Site.Proxy.Archive;
using Cms.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.Net.Http.Headers;
using Item = Cms.Endpoints.Site.Proxy.Archive.Item;
using Microsoft.AspNetCore.Authorization;
using Cms.Endpoints.Site.Atteribute;
using MediatR;
using Cmd.Application.Models.News.Commands.AddAsnad;
using Cmd.Application.Models.News.Queries.GetAllAsnad;

namespace Cms.Endpoints.Site.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowSpecific")]
public class MediaController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _archiveClient;
    private readonly ISender sender;

    public MediaController(IHttpClientFactory factory, ISender sender)
    {
        _httpClient = factory.CreateClient("Asnad");
        _archiveClient = factory.CreateClient("Archive");
        this.sender = sender;
    }


    [HttpGet("GetHistoricalPeriods")]
    public async Task<IActionResult> GetHistoricalPeriods()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetHistoricalPeriods");
        response.EnsureSuccessStatusCode(); // Ensure a successful response
        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }

    [HttpGet("GetListBaseTypes")]
    public async Task<IActionResult> GetListBaseTypes()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetListBaseTypes");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }

    [HttpGet("GetMediaEvents")]
    public async Task<IActionResult> GetMediaEvents()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetMediaEvents");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }

    [HttpGet("GetMediaLanguages")]
    public async Task<IActionResult> GetMediaLanguages()
    {

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");


        var response = await _httpClient.GetAsync("api/Media/GetMediaLanguages");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }

    [HttpGet("GetMediaPlaces")]
    public async Task<IActionResult> GetMediaPlaces()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetMediaPlaces");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("GetMediaSources")]
    public async Task<IActionResult> GetMediaSources()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetMediaSources");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("GetMediaStructures")]
    public async Task<IActionResult> GetMediaStructures()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetMediaStructures");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("GetPersonelInfos")]
    public async Task<IActionResult> GetPersonelInfos()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await _httpClient.GetAsync("api/Media/GetPersonelInfos");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("GetTypeWritingLines")]
    public async Task<IActionResult> GetTypeWritingLines()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1");
        _httpClient.DefaultRequestHeaders.Add("Language", "fa");
        _httpClient.DefaultRequestHeaders.Add("Referer", "https://asnad.isfahan.ir/home/document-list");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");


        var response = await _httpClient.GetAsync("api/Media/GetTypeWritingLines");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("GetPagedMediaItemsForView")]
    public async Task<IActionResult> GetPagedMediaItemsForView(int id, string? thematicCategoriesId, string? mediaPlaceId, string? mediaStructureId, string? mediaSourceId, string? typeWritingLineId, string? historicalPeriodId, string? mediaPersonelId, string? mediaLanguageId, string? relatedMediaId, string? mediaTypeId, string? title, string? tag, int offset = 0, int count = 12)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}api/Mediamanagement/GetPagedMediaItemsForView?thematicCategoriesId={thematicCategoriesId}&mediaPlaceId={mediaPlaceId}&mediaStructureId={mediaStructureId}&mediaSourceId={mediaSourceId}&typeWritingLineId={typeWritingLineId}&historicalPeriodId={historicalPeriodId}&mediaPersonelId={mediaPersonelId}&mediaLanguageId={mediaLanguageId}&relatedMediaId={relatedMediaId}&mediaTypeId={mediaTypeId}&title={title}&tag={tag}&offset={offset}&count={count}"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzOGM3MTljNy0wYTRlLTE5MTItMzI4ZC0wMDdiMTgzZmRiYTMiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlhdCI6MTcwMDExNTg3MiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Ik5pa2FuIiwiRGlzcGxheU5hbWUiOiLZhdiv24zYsduM2Kog2b7amNmI2YfYtNiMINiu2YTYp9mC24zYqiDZiCDZgdmG2KfZiNix24zigIzZh9in24wg2YbZiNuM2YYiLCJyZWplY3REZXNyaXB0aW9uIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiI0OGFhNDcyNDY3MTg0YjliOGRhZTNlZDY2NzRjNjM2NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiQ2l0aXplbiIsIkNvbXBhbnkiLCJHdWFyZCIsIlVzZXIiXSwibmJmIjoxNzAwMTE1ODcyLCJleHAiOjE3MDAxMTYxNzIsImF1ZCI6IkFueSJ9.Yx7wJly8u9DStfOYDKUrnVsTKj2JezOpwdFuNtqXLVE" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Referer", $"https://asnad.isfahan.ir/home/document-list" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                },
        };

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Root<Proxy.Asnad.Data>>(body);

            if (id is not 0)
            {
                var findModel = model.data.items.SingleOrDefault(t => t.id == id);
                model.data.items = new List<Site.Proxy.Asnad.Item>();
                model.data.items.Add(findModel);
            }

            return Ok(model);
        }
    }

    [HttpGet("GetTopMedia")]
    public async Task<IActionResult> GetTopMedia(int count)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}api/Mediamanagement/GetMostVisitedMedia?top={count}"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzOGM3MTljNy0wYTRlLTE5MTItMzI4ZC0wMDdiMTgzZmRiYTMiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlhdCI6MTcwMDExNTg3MiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Ik5pa2FuIiwiRGlzcGxheU5hbWUiOiLZhdiv24zYsduM2Kog2b7amNmI2YfYtNiMINiu2YTYp9mC24zYqiDZiCDZgdmG2KfZiNix24zigIzZh9in24wg2YbZiNuM2YYiLCJyZWplY3REZXNyaXB0aW9uIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiI0OGFhNDcyNDY3MTg0YjliOGRhZTNlZDY2NzRjNjM2NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiQ2l0aXplbiIsIkNvbXBhbnkiLCJHdWFyZCIsIlVzZXIiXSwibmJmIjoxNzAwMTE1ODcyLCJleHAiOjE3MDAxMTYxNzIsImF1ZCI6IkFueSJ9.Yx7wJly8u9DStfOYDKUrnVsTKj2JezOpwdFuNtqXLVE" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Referer", $"https://asnad.isfahan.ir/home/document-list" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                },
        };

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Root<Proxy.Asnad.Item[]>>(body);

            return Ok(model);
        }
    }

    [HttpGet("GetMediaDetails")]
    public async Task<IActionResult> GetMediaDetails(int id)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}api/Mediamanagement/GetMediaForView?id={id}&EditFor=false"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzOGM3MTljNy0wYTRlLTE5MTItMzI4ZC0wMDdiMTgzZmRiYTMiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlhdCI6MTcwMDExNTg3MiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Ik5pa2FuIiwiRGlzcGxheU5hbWUiOiLZhdiv24zYsduM2Kog2b7amNmI2YfYtNiMINiu2YTYp9mC24zYqiDZiCDZgdmG2KfZiNix24zigIzZh9in24wg2YbZiNuM2YYiLCJyZWplY3REZXNyaXB0aW9uIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiI0OGFhNDcyNDY3MTg0YjliOGRhZTNlZDY2NzRjNjM2NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiQ2l0aXplbiIsIkNvbXBhbnkiLCJHdWFyZCIsIlVzZXIiXSwibmJmIjoxNzAwMTE1ODcyLCJleHAiOjE3MDAxMTYxNzIsImF1ZCI6IkFueSJ9.Yx7wJly8u9DStfOYDKUrnVsTKj2JezOpwdFuNtqXLVE" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Referer", $"https://asnad.isfahan.ir/home/document-list" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                },
        };

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Root<Details>>(body);

            return Ok(model);
        }
    }

    [HttpGet("GetAttachments")]
    [AuthorizeToken]
    public async Task<IActionResult> GetAttachments(string attachmentGroup)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}api/Attachment/GetAllAttachment?guid={attachmentGroup}"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzOGM3MTljNy0wYTRlLTE5MTItMzI4ZC0wMDdiMTgzZmRiYTMiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlhdCI6MTcwMDExNTg3MiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Ik5pa2FuIiwiRGlzcGxheU5hbWUiOiLZhdiv24zYsduM2Kog2b7amNmI2YfYtNiMINiu2YTYp9mC24zYqiDZiCDZgdmG2KfZiNix24zigIzZh9in24wg2YbZiNuM2YYiLCJyZWplY3REZXNyaXB0aW9uIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiI0OGFhNDcyNDY3MTg0YjliOGRhZTNlZDY2NzRjNjM2NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiQ2l0aXplbiIsIkNvbXBhbnkiLCJHdWFyZCIsIlVzZXIiXSwibmJmIjoxNzAwMTE1ODcyLCJleHAiOjE3MDAxMTYxNzIsImF1ZCI6IkFueSJ9.Yx7wJly8u9DStfOYDKUrnVsTKj2JezOpwdFuNtqXLVE" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Referer", $"https://asnad.isfahan.ir/home/document-list" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                },
        };

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Root<List<Attachment>>>(body);

            return Ok(model);
        }
    }

    [HttpGet("GetImages")]
    public async Task<IActionResult> GetImage(string folderName, string fileName)
    {
        var response = await _httpClient.GetByteArrayAsync($"uploads/Resources/File/{folderName}/{fileName}");
        byte[] imageBytes = response;

        return File(imageBytes, "image/jpeg");
    }

    [HttpGet("GetImagesByUrl")]
    public async Task<IActionResult> GetImage(string url)
    {
        var response = await _httpClient.GetByteArrayAsync(url);
        byte[] imageBytes = response;

        return File(imageBytes, "image/jpeg");
    }

    [HttpPost("GetArchive")]
    public async Task<IActionResult> GetArchive(Request request)
    {
        var data = new
        {
            limit = request.Limit,
            offset = request.Offset,
            MainValues = new[]
    {
        new
        {
            key = request.Key,
            value = request.Value,
            @operator = request.Operator
        }
    }
        };

        var jsonInString = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");


        var requestData = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{_archiveClient.BaseAddress.AbsoluteUri}api/content/file-list"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJQcmltYXJ5U2lkIjoiMSIsIkd1aWQiOiIzNzE0ZmVmNy02MjgxLTQyYWUtYjBhMC0yMWJjYzBmNGVhMDQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTg3MjUzMjM5M30.cfe23flH9STpd3UHARGZznEjSSJTCCK_nToUGkyuxUQ" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Token", "91b8011e-c5ed-4b7a-bd1c-2e00cad8adc6" },
                    { "Referer", $"{_archiveClient.BaseAddress.AbsoluteUri}" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                },
            Content = content
        };

        using (var response = await _archiveClient.SendAsync(requestData))
        {
            if (!response.IsSuccessStatusCode)
            {
                return Ok("Server Not Respond....");
            }

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Proxy.Archive.Data>(body);

            if (request.Id is not 0)
            {
                var findModel = model.list.SingleOrDefault(t => t.PkFileContent == request.Id);
                model.list = new List<Proxy.Archive.Item>();
                model.list.Add(findModel);
            }

            return Ok(model);
        }
    }

    [HttpGet("GetTopArchive")]
    public async Task<IActionResult> GetTopArchive(int count)
    {

        var requestData = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_archiveClient.BaseAddress.AbsoluteUri}api/content/favorite-list"),
            Headers =
                {
                    { "Accept", "application/json, text/plain, */*" },
                    { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                    { "token", "628966cb-42b3-4467-99ff-a8d169644123" },
                    { "Connection", "keep-alive" },
                    { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                    { "Language", "fa" },
                    { "Referer", $"{_archiveClient.BaseAddress.AbsoluteUri}" },
                    { "Sec-Fetch-Dest", "empty" },
                    { "Sec-Fetch-Mode", "cors" },
                    { "Sec-Fetch-Site", "same-origin" },
                    { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" },
                    { "sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"" },
                    { "sec-ch-ua-mobile", "?0" },
                    { "sec-ch-ua-platform", "Windows" },
                }
        };

        using (var response = await _archiveClient.SendAsync(requestData))
        {
            if (!response.IsSuccessStatusCode)
            {
                return Ok("Server Not Respond....");
            }

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<TopArchive>(body);
            model.VideoFile = model.VideoFile.OrderByDescending(t => t.PkFileContent).Take(count).ToList()
                ;
            return Ok(model);
        }
    }

    [HttpGet("GetArchiveImage")]
    public async Task<IActionResult> GetArchiveImage(string url)
    {
        var response = await _archiveClient.GetByteArrayAsync(url);
        byte[] imageBytes = response;

        if (url.EndsWith(".mp4"))
        {
            return File(imageBytes, "video/mp4", Guid.NewGuid().ToString().Substring(0, 10));
        }

        return File(imageBytes, "image/jpeg");
    }

    [HttpGet("AlphabeticalSearch")]
    public async Task<IActionResult> AlphabeticalSearch(string alphabet, int count, int offset)
    {
        var query = new GetAllAsnadQuery
        {
            Alephbatic = alphabet,
            PageNumber = offset + 1,
            PageSize = count
        };

        var result = await sender.Send(query);

        var resultView = new Root<Cms.Endpoints.Site.Proxy.Asnad.Data>
        {
            data = new Proxy.Asnad.Data
            {
                items = result.QueryResult.Select(t => new Proxy.Asnad.Item
                {
                    id = t.Id,
                    title = t.Title,
                    details = t.Description,
                    thumbnailUrl = t.ImageName,
                    accessNumber = 0,
                    actors = 0,
                    appendicesMedia = 0,
                    attachmentCount = 0,
                    attachmentGroup = "",
                    author = 0,
                    body = "",
                    cameraMan = 0,
                    cameraModel = 0,
                    clicks = 0,
                    commentIsActive = true,
                    compositor = 0,
                    countPage = 0,
                    createdByUser = 0,
                    createdOnDate = DateTime.Now,
                    dateOfDocumentDescription = DateTime.Now,
                    director = 0,
                    editor = 0,
                    @event = 0,
                    eventId = 0,
                    favoriteDescription = "",
                    historicalPeriod = 0,
                    historicalPeriodId = 0,
                    imageUrl = "",
                    isActive = true,
                    isFavorite = true,
                    locations = "",
                    manufactureDate = DateTime.Now,
                    mediaAccessType = 0,
                    mediaLanguage = 0,
                    mediaLanguageId = 0,
                    mediaSource = 0,
                    mediaSourceId = 0,
                    mediaStructure = 0,
                    mediaStructureId = 0,
                    mediaTypes = 0,
                    meidaFields = 0,
                    numberOfRowsAndArchive = "",
                    orator = 0,
                    personnel = 0,
                    personnelIDs = 0,
                    personnels = 0,
                    photographer = 0,
                    producer = 0,
                    rate = 0,
                    relatedMedia = 0,
                    relatedMediaId = 0,
                    studio = 0,
                    tags = "",
                    thematicCategories = 0,
                    typeWritingLine = 0,
                    typeWritingLineId = 0,
                    waterMarkImage = 0,
                    waterMarkImageId = 0
                }).ToList(),
                totalItems = 0
            },
            isSuccess = true,
            messages = "عملیات با موفقیت انجام شد",
            statusCode = 200,
            errors = ""
        };

        return Ok(resultView);
    }

    [HttpGet("UpdateAsnad")]
    public async Task<IActionResult> UpdateAsnad()
    {
        await sender.Send(new AddAsnadCommand());

        return Ok("Update successfully.");
    }
}