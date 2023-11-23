namespace Core.Common;

public class PaginatedResult<TData>
{
    public PaginatedResult(ICollection<TData> data, Pagination pagination)
    {
        this.Data = data;
        this.Pagination = pagination;
    }

    public ICollection<TData> Data { get; }

    public Pagination Pagination { get; }
}