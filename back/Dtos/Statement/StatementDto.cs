using System.Collections.Generic;

namespace Politics.Dtos
{
  public class StatementDto
  {
    public string PoliticianId { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<TagOutDto> Tags { get; set; }
  }
}
