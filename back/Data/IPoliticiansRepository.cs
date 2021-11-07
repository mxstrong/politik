using System.Threading.Tasks;
using Politics.Dtos;
using Politics.Helpers;

namespace Politics.Data
{
  public interface IPoliticiansRepository
  {
    Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto);
    Task<PaginatedList<PoliticianOutDto>> GetAllPoliticians(int? pageNumber, int? pageSize);
    Task<PoliticianOutDto> DeletePolitician(string id);
    Task<PoliticianOutDto> GetPoliticianById(string id);
  }
}