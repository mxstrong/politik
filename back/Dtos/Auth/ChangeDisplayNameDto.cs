using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Dtos
{
  public class ChangeDisplayNameDto
  {
    public string UserId { get; set; }
    public string NewDisplayName { get; set; }
  }
}
