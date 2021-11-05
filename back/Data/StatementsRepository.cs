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
    private readonly ITagsRepository _tagsRepo;

    public StatementsRepository(PoliticsDbContext context, IMapper mapper, ITagsRepository tagsRepo)
    {
      _context = context;
      _mapper = mapper;
      _tagsRepo = tagsRepo;
    }
    public async Task<StatementOutDto> AddStatement(StatementDto statementDto)
    {
      var statement = _mapper.Map<StatementDto, Statement>(statementDto);
      statement.StatementId = Guid.NewGuid().ToString();
      statement.CreatedAt = DateTime.Now;
      statement.CreatedById = "test";
      
      await _context.Statements.AddAsync(statement);
      
      var statementTags = new List<StatementTag>();
      statementDto.Tags.ForEach(tag =>
      {
        if (tag.TagId is null)
        {
          var newTag = _mapper.Map<TagOutDto, TagDto>(tag);
          var addTagTask = Task.Run<TagOutDto>(async () => await _tagsRepo.AddTag(newTag));
          var addedTag = addTagTask.Result;
          statementTags.Add(new StatementTag()
          {
            StatementTagId = Guid.NewGuid().ToString(),
            TagId = addedTag.TagId,
            StatementId = statement.StatementId
          });
        } else
        {
          statementTags.Add(new StatementTag()
          {
            StatementTagId = Guid.NewGuid().ToString(),
            TagId = tag.TagId,
            StatementId = statement.StatementId
          });
        }
      });
        
      await _context.StatementTags.AddRangeAsync(statementTags);
      await _context.SaveChangesAsync();
      var createdStatement = await _context.Statements.FindAsync(statement.StatementId);
      return _mapper.Map<Statement, StatementOutDto>(createdStatement);
    }

    public async Task<List<StatementOutDto>> GetAllStatements(string? politicianId, List<string>? tags)
    {
      var statements = new List<Statement>();
      if (politicianId is not null)
      {
        statements = await _context.Statements
        .Where(statement => statement.PoliticianId == politicianId)
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag)
        .ToListAsync();
      } else
      {
        statements = await _context.Statements
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag)
        .ToListAsync();
      }
      if (tags is not null && tags.Count > 0)
      {
        statements = statements.Where(statement => statement.StatementTags.Any(tag => tags.Contains(tag.TagId))).ToList();
      }
      var sortedStatements = statements.OrderByDescending(statement => statement.CreatedAt).ToList();
      return _mapper.Map<List<Statement>, List<StatementOutDto>>(sortedStatements);
    }

    public async Task<StatementOutDto> GetStatementById(string id)
    {
      var statement = await _context.Statements
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag)
        .SingleAsync(statement => statement.StatementId == id);

      return _mapper.Map<Statement, StatementOutDto>(statement);
    }

    public async Task<StatementOutDto> DeleteStatementById(string id)
    {
      var statementToDelete = await _context.Statements.FindAsync(id);
      if (statementToDelete is null)
      {
        return null;
      }
      _context.Statements.Remove(statementToDelete);
      await _context.SaveChangesAsync();
      return _mapper.Map<Statement, StatementOutDto>(statementToDelete);
    }
  }
}
