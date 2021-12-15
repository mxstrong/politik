using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public class PartiesRepository : IPartiesRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;
    public PartiesRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public async Task<PartyOutDto> AddParty(PartyDto partyDto, string userId)
    {
      var party = _mapper.Map<PartyDto, Party>(partyDto);
      party.PartyId = Guid.NewGuid().ToString();
      party.CreatedAt = DateTime.Now;
      party.CreatedById = userId;
      await _context.AddAsync(party);
      await _context.SaveChangesAsync();
      var addedParty = await _context.Parties.FindAsync(party.PartyId);
      if (addedParty is null)
      {
        return null;
      }
      return _mapper.Map<Party, PartyOutDto>(addedParty);
    }

    public async Task<PartyOutDto> DeleteParty(string id)
    {
      var party = await _context.Parties.FindAsync(id);
      if (party is null)
      {
        return null;
      }
      _context.Remove(party);
      await _context.SaveChangesAsync();
      return _mapper.Map<Party, PartyOutDto>(party);
    }

    public async Task<List<PartyOutDto>> GetAllParties()
    {
      var parties = await _context.Parties.ToListAsync();
      return _mapper.Map<List<Party>, List<PartyOutDto>>(parties);
    }

    public async Task<PartyOutDto> GetPartyById(string id)
    {
      var party = await _context.Parties.FindAsync(id);
      if (party is not null)
      {
        return _mapper.Map<Party, PartyOutDto>(party);
      }
      return null;
    }
  }
}
