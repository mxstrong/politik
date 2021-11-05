using Politics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IPartiesRepository
  {
    Task<List<PartyOutDto>> GetAllParties();
    Task<PartyOutDto> GetPartyById(string id);
    Task<PartyOutDto> AddParty(PartyDto partyDto);
    Task<PartyOutDto> DeleteParty(string id);
  }
}
