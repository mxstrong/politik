namespace Politics.Helpers
{
  public abstract class PaginationParams
  {
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
  }
}
