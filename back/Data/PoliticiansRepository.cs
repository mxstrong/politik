using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Politics.Dtos;

namespace Politics.Data
{
  public class PoliticiansRepository : IPoliticiansRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public PoliticiansRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<List<PoliticianOutDto>> GetAllPoliticians()
    {
      var politicians = await _context.Politicians.ToListAsync();
      return _mapper.Map<List<Politician>, List<PoliticianOutDto>>(politicians);
    }

    public async Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto)
    {
      var politician = _mapper.Map<PoliticianDto, Politician>(politicianDto);
      politician.PoliticianId = Guid.NewGuid().ToString();
      politician.CreatedAt = DateTime.Now;
      politician.CreatedBy = "test";
      if (politician.PartyId is not null)
      {
        var party = await _context.Parties.FindAsync(politician.PartyId);
        if (party is null)
        {
          return null;
        }
      }
      var createdPolitician = (await _context.Politicians.AddAsync(politician)).Entity;
      await _context.SaveChangesAsync();
      return _mapper.Map<Politician, PoliticianOutDto>(createdPolitician);
    }

    public async Task DeletePolitician(string id)
    {
      var politician = await _context.Politicians.FindAsync(id);
      _context.Politicians.Remove(politician);
      await _context.SaveChangesAsync();
    }
  }
}
