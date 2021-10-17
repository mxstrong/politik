using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Dtos
{
  public class PoliticianDto
  {
    public string? PartyId { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
  }
}
