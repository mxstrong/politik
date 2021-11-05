namespace Politics.Model
{
  public class StatementTag
  {
    public string StatementTagId { get; set; }
    public string StatementId { get; set; }
    public string TagId { get; set; }
    public Statement Statement { get; set; }
    public Tag Tag { get; set; }
  }
}
