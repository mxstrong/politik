using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Party
  {
    [Required]
    public string PartyId { get; set; }
    [Required]
    public string LongName { get; set; }
    [Required]
    public string ShortName { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string CreatedById { get; set; }
    public string UpdatedById { get; set; }
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
  }
}
