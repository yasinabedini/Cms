using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;

namespace Cmd.Application.Models.News.Queries.GetAll
{
    public class GetAllNewsQuery : PageQuery<PagedData<NewsViewModel>>
    {
        public long? TypeId { get; set; }
        public long? LanguageId { get; set; }
        public bool IsPage { get; set; }
    }
}
