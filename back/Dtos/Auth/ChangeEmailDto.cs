using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Dtos
{
  public class ChangeEmailDto
  {
    public string UserId { get; set; }
    public string NewEmail { get; set; }
  }
}
