using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Party
  {
    [Required]
    public string Partyid { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
  }
}
