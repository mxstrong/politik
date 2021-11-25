namespace Politics.Dtos
{
  public class PoliticianOutDto
  {
    public string Id { get; set; }
    public PartyOutDto Party { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
  }
}