using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Dtos
{
  public class StatementDto
  {
    public string PoliticianId { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<string> TagIds { get; set; }
  }
}
