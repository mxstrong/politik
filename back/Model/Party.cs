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
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
  }
}
