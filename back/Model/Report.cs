using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Report
  {
    [Required]
    public string ReportId { get; set; }
    [Required]
    public string StatementId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public bool IsReviewed { get; set; }
    [Required]
    public string CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public string ReviewedById { get; set; }
    public User ReviewedBy { get; set; }
  }
}
