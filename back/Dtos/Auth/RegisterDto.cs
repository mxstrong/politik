using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Dtos
{
  public class RegisterDto
  {
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify a password between 8 and 20 characters.")]
    public string Password { get; set; }
  }
}
