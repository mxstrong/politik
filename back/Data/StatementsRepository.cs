using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Helpers;
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
    public async Task<StatementOutDto> AddStatement(StatementDto statementDto, string userId)
    {
      var statement = _mapper.Map<StatementDto, Statement>(statementDto);
      statement.StatementId = Guid.NewGuid().ToString();
      statement.CreatedAt = DateTime.Now;
      statement.CreatedById = userId;
      
      await _context.Statements.AddAsync(statement);
      
      var statementTags = new List<StatementTag>();
      statementDto.Tags.ForEach(tag =>
      {
        if (tag.TagId is null)
        {
          var newTag = _mapper.Map<TagOutDto, TagDto>(tag);
          var addTagTask = Task.Run<TagOutDto>(async () => await _tagsRepo.AddTag(newTag, userId));
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

    public async Task<PaginatedList<StatementOutDto>> GetAllStatements(string? politicianId, List<string>? tags, int? pageNumber, int? pageSize)
    {
      IQueryable<Statement> statements;
      if (politicianId is not null)
      {
        statements = _context.Statements
        .Where(statement => statement.PoliticianId == politicianId)
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag);
      } else
      {
        statements = _context.Statements
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag);
      }
      if (tags is not null && tags.Count > 0)
      {
        statements = statements.Where(statement => statement.StatementTags.Any(tag => tags.Contains(tag.TagId)));
      }
      var sortedStatements = statements.OrderByDescending(statement => statement.CreatedAt);
      var paginatedStatements = await PaginatedList<Statement>.CreateAsync(sortedStatements, pageNumber ?? 1, pageSize ?? 10);
      return _mapper.Map<PaginatedList<Statement>, PaginatedList<StatementOutDto>>(paginatedStatements);
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

    public async Task<Statement> GetStatementEntityById(string id)
    {
      var statement = await _context.Statements
        .Include(statement => statement.CreatedBy)
        .Include(statement => statement.Politician)
        .Include(statement => statement.StatementTags)
        .ThenInclude(statementTag => statementTag.Tag)
        .SingleAsync(statement => statement.StatementId == id);

      return statement;
    }

    public async Task<StatementOutDto> DeleteStatementById(string id)
    {
      var statementToDelete = await _context.Statements.FindAsync(id);
      if (statementToDelete is null)
      {
        return null;
      }
      var statementTags = await _context.StatementTags.Where(statementTag => statementTag.StatementId == id).ToListAsync();
      _context.Statements.Remove(statementToDelete);
      _context.StatementTags.RemoveRange(statementTags);
      await _context.SaveChangesAsync();
      return _mapper.Map<Statement, StatementOutDto>(statementToDelete);
    }
  }
}
