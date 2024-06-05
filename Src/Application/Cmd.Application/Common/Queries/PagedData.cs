namespace Cmd.Application.Common.Queries;
/// <summary>
/// ساختار پایه جهت بازگشت داده‌ها هنگام کوئری گرفتن وقتی که Paging دارد
/// </summary>
/// <typeparam name="T">نوع داده‌ای که از کوئری دریافت می‌شود را تعیین می‌کند!</typeparam>
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

        double pageCount;

        pageCount = TotalCount / PageSize;

        if (pageCount < 0)
        {
            pageCount = 1;
        }
        else if (TotalCount % PageSize != 0)
        {
            pageCount = pageCount + 1;
        }

        return (int)pageCount;
    }

}
