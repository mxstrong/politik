using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class User
  {
    [Required]
    public string UserId { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public byte[] PasswordHash { get; set; }
    [Required]
    public byte[] PasswordSalt { get; set; }
    [Required]
    public string RoleId { get; set; }
    public Role Role { get; set; }
    [Required]
    public bool Activated { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
