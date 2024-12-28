namespace HouseBrokerApplication.Shared.Helpers;

/// <summary>
/// shared generic Paginated helper method for pagination 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T>
{
    public int TotalCount { get; private set; }
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }

    // calculate total number of pages baed on total count and size.
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize); 

    public List<T> Items { get; private set; } = new List<T>();
    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        Items.AddRange(items);
    }

}
