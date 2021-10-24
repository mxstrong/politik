using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Data
{
  public class StatementsRepository : IStatementsRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public StatementsRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public async Task<StatementOutDto> AddStatement(StatementDto statementDto)
    {
      var statement = _mapper.Map<StatementDto, Statement>(statementDto);
      statement.StatementId = Guid.NewGuid().ToString();
      statement.CreatedAt = DateTime.Now;
      statement.CreatedById = "test";
      
      await _context.Statements.AddAsync(statement);
      
      var statementTags = new List<StatementTag>();
      statementDto.TagIds.ForEach(tagId =>
        statementTags.Add(new StatementTag() {
          StatementTagId = Guid.NewGuid().ToString(),
          TagId = tagId,
          StatementId = statement.StatementId
        }));
      await _context.StatementTags.AddRangeAsync(statementTags);
      await _context.SaveChangesAsync();
      var createdStatement = await _context.Statements.FindAsync(statement.StatementId);
      return _mapper.Map<Statement, StatementOutDto>(createdStatement);
    }

    public Task<List<StatementOutDto>> GetAllStatements()
    {
      throw new NotImplementedException();
    }
  }
}
