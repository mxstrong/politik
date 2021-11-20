using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
