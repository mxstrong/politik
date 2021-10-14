using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Role
  {
    [Required]
    public string RoleId { get; set; }
    [Required]
    public string Name { get; set; }
  }
}
