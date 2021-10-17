using System.Collections.Generic;
using System.Threading.Tasks;
using Politics.Dtos;

namespace Politics.Data
{
  public interface IPoliticiansRepository
  {
    Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto);
    Task<List<PoliticianOutDto>> GetAllPoliticians();
    Task DeletePolitician(string id);
  }
}