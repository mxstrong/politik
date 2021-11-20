using System.Threading.Tasks;
using Politics.Dtos;
using Politics.Helpers;

namespace Politics.Data
{
  public interface IPoliticiansRepository
  {
    Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto, string userId);
    Task<PaginatedList<PoliticianOutDto>> GetAllPoliticians(int? pageNumber, int? pageSize, string? partyId);
    Task<PoliticianOutDto> DeletePolitician(string id);
    Task<PoliticianOutDto> UpdatePolitician(string politicianId, UpdatePoliticianDto politicianDto, string userId);
    Task<PoliticianOutDto> GetPoliticianById(string id);
  }
}