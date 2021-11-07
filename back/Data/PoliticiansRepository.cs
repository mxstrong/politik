﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Model;
using System;
using System.Threading.Tasks;
using Politics.Dtos;
using Politics.Helpers;

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

    public async Task<PaginatedList<PoliticianOutDto>> GetAllPoliticians(int? pageNumber, int? pageSize)
    {
      var politicians = await PaginatedList<Politician>.CreateAsync(_context.Politicians.Include(politician => politician.Party), pageNumber ?? 1, pageSize ?? 10);
      return _mapper.Map<PaginatedList<Politician>, PaginatedList<PoliticianOutDto>>(politicians);
    }

    public async Task<PoliticianOutDto> GetPoliticianById(string id)
    {
      var politician = await _context.Politicians.Include(politician => politician.Party).SingleAsync(p => p.PoliticianId == id);
      return _mapper.Map<Politician, PoliticianOutDto>(politician);
    }

    public async Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto)
    {
      var politician = _mapper.Map<PoliticianDto, Politician>(politicianDto);
      politician.PoliticianId = Guid.NewGuid().ToString();
      politician.CreatedAt = DateTime.Now;
      politician.CreatedById = "test";
      if (politician.PartyId is not null)
      {
        var party = await _context.Parties.FindAsync(politician.PartyId);
        if (party is null)
        {
          return null;
        }
        politician.Party = party;
      }
      await _context.Politicians.AddAsync(politician);
      await _context.SaveChangesAsync();

      var createdPolitician = await _context.Politicians.FindAsync(politician.PoliticianId);
      if (createdPolitician is null)
      {
        return null;
      }
      return _mapper.Map<Politician, PoliticianOutDto>(createdPolitician);
    }

    public async Task<PoliticianOutDto> DeletePolitician(string id)
    {
      var politician = await _context.Politicians.FindAsync(id);
      if (politician is null)
      {
        return null;
      }
      _context.Politicians.Remove(politician);
      await _context.SaveChangesAsync();
      return _mapper.Map<Politician, PoliticianOutDto>(politician);
    }
  }
}
