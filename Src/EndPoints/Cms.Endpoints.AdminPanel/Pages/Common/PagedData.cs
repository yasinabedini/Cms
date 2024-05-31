namespace Cms.Endpoints.AdminPanel.Pages.Common
{
    public class PagedData<T>
    {
        public List<T> QueryResult { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int pageCount => calculatePageCount();

        public int calculatePageCount()
        {
            if (PageNumber is 0 || PageSize is 0)
            {
                PageNumber = 1;
                PageSize = 20;
            }
            int pageCount = TotalCount / PageSize;
            if (pageCount <= 1)
            {
                pageCount = 1;
            }
            if (TotalCount % PageSize > 0)
            {
                pageCount++;
            }

            return pageCount;
        }

    }
}
