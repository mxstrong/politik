namespace Politics.Dtos
{
  public class ReportOutDto
  {
    public string ReportId { get; set; }
    public string StatementId { get; set; }
    public string Description { get; set; }
    public bool IsReviewed { get; set; }
    public UserProfileDto CreatedBy { get; set;  }
    public UserProfileDto ReviewedBy { get; set; }
  }
}
