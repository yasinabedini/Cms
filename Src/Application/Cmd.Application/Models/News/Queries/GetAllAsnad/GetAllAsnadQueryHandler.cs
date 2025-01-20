using Cmd.Application.Common.Queries;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAllAsnad
{
    public class GetAllAsnadQueryHandler : IQueryHandler<GetAllAsnadQuery, PagedData<Asnad>>
    {
        private readonly INewsRepository newsRepository;

        public GetAllAsnadQueryHandler(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public Task<PagedData<Asnad>> Handle(GetAllAsnadQuery request, CancellationToken cancellationToken)
        {
            var Asnads = newsRepository.GetAllAsnad();

            if (!string.IsNullOrEmpty(request.Alephbatic))
            {
                Asnads = Asnads.Where(t => t.Title.StartsWith(request.Alephbatic)).ToList();
            }

            Asnads = Asnads.Skip(request.SkipCount).Take(request.PageSize).ToList();

            return Task.FromResult(new PagedData<Asnad>
            {
                QueryResult = Asnads.ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = Asnads.Count()
            });
        }
    }
}
