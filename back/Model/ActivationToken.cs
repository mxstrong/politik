using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class ActivationToken
  {
    [Required]
    public string Id { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
  }
}
