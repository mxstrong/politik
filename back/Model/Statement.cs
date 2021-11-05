using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Statement
  {
    [Required]
    public string StatementId { get; set; }
    [Required]
    public string PoliticianId { get; set; }
    [Required]
    public string Link { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string CreatedById { get; set; }
    public string? UpdatedById { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Politician Politician { get; set; }
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public List<StatementTag> StatementTags { get; set; }
  }
}
