namespace Politics.Helpers
{
  public abstract class PaginationParams
  {
    public int? pageNumber { get; set; }
    public int? pageSize { get; set; }
  }
}
