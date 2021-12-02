using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class EmailChangeToken
  {
      [Required]
      public string Id { get; set; }
      [Required]
      public string UserId { get; set; }
      [Required]
      public string NewEmail { get; set; }
      [Required]
      public DateTime CreatedAt { get; set; }
  }
}
