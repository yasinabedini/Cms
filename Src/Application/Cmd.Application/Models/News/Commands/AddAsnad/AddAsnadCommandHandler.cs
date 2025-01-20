using Cmd.Application.Common.Commands;
using Cms.Domain.Models.News.Repository;
using Cms.Endpoints.Site.Proxy.Asnad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Cmd.Application.Models.News.Commands.AddAsnad
{
    public class AddAsnadCommandHandler : ICommandHandler<AddAsnadCommand>
    {
        private readonly INewsRepository newsRepository;
        private readonly HttpClient _httpClient;

        public AddAsnadCommandHandler(INewsRepository newsRepository, HttpClient httpClient)
        {
            this.newsRepository = newsRepository;
            _httpClient = httpClient;
        }

        public async Task Handle(AddAsnadCommand request2, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://asnad.isfahan.ir/api/Mediamanagement/GetPagedMediaItemsForView?thematicCategoriesId=null&mediaPlaceId=null&mediaStructureId=null&mediaSourceId=null&typeWritingLineId=null&historicalPeriodId=null&mediaPersonelId=null&mediaLanguageId=null&relatedMediaId=null&mediaTypeId=undefined&title=null&tag=&offset=0&count=400"),
                Headers =
            {
                { "Accept", "application/json, text/plain, */*" },
                { "Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7" },
                { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzOGM3MTljNy0wYTRlLTE5MTItMzI4ZC0wMDdiMTgzZmRiYTMiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlhdCI6MTcwMDExNTg3MiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Ik5pa2FuIiwiRGlzcGxheU5hbWUiOiLZhdiv24zYsduM2Kog2b7amNmI2YfYtNiMINiu2YTYp9mC24zYqiDZiCDZgdmG2KfZiNix24zigIzZh9in24wg2YbZiNuM2YYiLCJyZWplY3REZXNyaXB0aW9uIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiI0OGFhNDcyNDY3MTg0YjliOGRhZTNlZDY2NzRjNjM2NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiQ2l0aXplbiIsIkNvbXBhbnkiLCJHdWFyZCIsIlVzZXIiXSwibmJmIjoxNzAwMTE1ODcyLCJleHAiOjE3MDAxMTYxNzIsImF1ZCI6IkFueSJ9.Yx7wJly8u9DStfOYDKUrnVsTKj2JezOpwdFuNtqXLVE" },
                { "Connection", "keep-alive" },
                { "Cookie", "_ga_H8YK9ZGSZF=GS1.1.1659516470.5.0.1659516470.0; cookiesession1=678B289D73DD25B62A4FCFF05E9774D1" },
                { "Language", "fa" },
                },
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                body = body.Replace("null", "0");

                var model = JsonConvert.DeserializeObject<Root<Cms.Endpoints.Site.Proxy.Asnad.Data>>(body);

                newsRepository.DeleteAllAsnad();

                foreach (var item in model.data.items)
                {
                    newsRepository.AddAsnad(item.id, item.title, item.details, item.thumbnailUrl);
                }

            }
        }
    }
}
