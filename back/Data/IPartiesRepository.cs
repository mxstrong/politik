using Microsoft.AspNetCore.Mvc;
using Politics.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
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
