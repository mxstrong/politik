using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IPoliticiansRepository
  {
    Task<PoliticianDto> AddPolitician(PoliticianDto politicianDto);
    Task<List<PoliticianDto>> GetAllPoliticians();
  }
}