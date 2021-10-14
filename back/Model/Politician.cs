using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Politician
  {
    [Required]
    public string PoliticianId { get; set; }
    public string PartyId { get; set; }
    public Party Party { get; set; }
    [Required]
    public string FullName { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
  }
}
