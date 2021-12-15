using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Like
  {
    [Required]
    public string LikeId { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public string StatementId { get; set; }
  }
}
