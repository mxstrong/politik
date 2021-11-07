using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using Politics.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Politics.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StatementsController : ControllerBase
  {
    private readonly IStatementsRepository _statementsRepo;
    private readonly IPoliticiansRepository _politiciansRepo;

    public StatementsController(IStatementsRepository statementsRepo, IPoliticiansRepository politiciansRepo)
    {
      _statementsRepo = statementsRepo;
      _politiciansRepo = politiciansRepo;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<StatementOutDto>>> GetAllStatements([FromQuery] StatementsParams parameters)
    {
      var statements = await _statementsRepo.GetAllStatements(parameters.politician, parameters.tags, parameters.pageNumber, parameters.pageSize);
      var paginationMetadata = new
      {
        statements.Count,
        statements.PageSize,
        statements.PageIndex,
        statements.TotalPages,
        statements.HasNextPage,
        statements.HasPreviousPage
      };
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

      return Ok(statements);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<StatementOutDto>> GetStatetementById(string id)
    {
      return Ok(await _statementsRepo.GetStatementById(id));
    }
    [HttpPost]
    public async Task<ActionResult<StatementOutDto>> AddStatement(StatementDto statementDto)
    {
      if (statementDto.PoliticianId is null)
      {
        return ValidationProblem("Nenurodėte politiko kurio pasisakymą bandėte pridėti");
      }
      if (statementDto.Link is null || statementDto.Link.Length < 1)
      {
        return ValidationProblem("Nenurodėte pasisakymo nuorodos");
      }
      if (statementDto.Link.Length > 250)
      {
        return ValidationProblem("Nurodyta nuoroda per ilga (maksimalus ilgis 250 simbolių)");
      }
      if (statementDto.Tags is null || statementDto.Tags.Count < 1)
      {
        return ValidationProblem("Reikia pasisakymui parinkti bent vieną žymą");
      }
      var existingPolitician = await _politiciansRepo.GetPoliticianById(statementDto.PoliticianId);
      if (existingPolitician is null)
      {
        return ValidationProblem("Nurodytas politikas neegzistuoja");
      }
      var createdStatement = await _statementsRepo.AddStatement(statementDto);
      return CreatedAtAction(nameof(GetStatetementById), new { id = createdStatement.StatementId }, createdStatement);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<StatementOutDto>> DeleteStatement(string id)
    {
      var deletedStatement =  await _statementsRepo.DeleteStatementById(id);
      if (deletedStatement is null)
      {
        return ValidationProblem("Nurodytas pasisakymas nerastas");
      }
      return deletedStatement;
    }
  }

  public class StatementsParams : PaginationParams
  {
    public string? politician { get; set; }
    public List<string>? tags { get; set; }
  }
}
