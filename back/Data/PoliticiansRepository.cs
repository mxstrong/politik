using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<List<PoliticianDto>> GetAllPoliticians()
    {
      var politicians = await _context.Politicians.ToListAsync();
      return _mapper.Map<List<Politician>, List<PoliticianDto>>(politicians);
    }

    public async Task<PoliticianDto> AddPolitician(PoliticianDto politicianDto)
    {
      var politician = _mapper.Map<PoliticianDto, Politician>(politicianDto);
      politician.PoliticianId = Guid.NewGuid().ToString();
      politician.CreatedAt = DateTime.Now;
      politician.CreatedBy = "test";
      politician.PartyId = null;
      var createdPolitician = (await _context.Politicians.AddAsync(politician)).Entity;
      await _context.SaveChangesAsync();
      return _mapper.Map<Politician, PoliticianDto>(createdPolitician);
    }
  }
}
