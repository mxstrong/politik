using System;
using System.Collections.Generic;

namespace Politics.Model
{
  public class Tag
  {
    public string TagId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedById { get; set; }
    public User CreatedBy{ get; set; }
    public ICollection<Statement> Statements { get; set; }
    public List<StatementTag> StatementTags { get; set; }
  }
}
