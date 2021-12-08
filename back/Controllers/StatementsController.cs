using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using Politics.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Politics.Services;

namespace Politics.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StatementsController : ControllerBase
  {
    private readonly IStatementsRepository _statementsRepo;
    private readonly IPoliticiansRepository _politiciansRepo;
    private readonly IAuthService _authService;

    public StatementsController(IStatementsRepository statementsRepo, IPoliticiansRepository politiciansRepo, IAuthService authService)
    {
      _statementsRepo = statementsRepo;
      _politiciansRepo = politiciansRepo;
      _authService = authService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<StatementOutDto>>> GetAllStatements([FromQuery] StatementsParams parameters)
    {
      var statements = await _statementsRepo.GetAllStatements(parameters.Politician, parameters.Tags, parameters.PageNumber, parameters.PageSize);
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
    [Authorize]
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
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var createdStatement = await _statementsRepo.AddStatement(statementDto, userId);
      return CreatedAtAction(nameof(GetStatetementById), new { id = createdStatement.StatementId }, createdStatement);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<StatementOutDto>> DeleteStatement(string id)
    {
      var statementToDelete = await _statementsRepo.GetStatementEntityById(id);
      var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (role != "Admin" && role != "Mod" && userId != statementToDelete.CreatedById)
      {
        return Unauthorized();
      }
      var deletedStatement = await _statementsRepo.DeleteStatementById(id);
      if (deletedStatement is null)
      {
        return ValidationProblem("Nurodytas pasisakymas nerastas");
      }
      return deletedStatement;
    }

    [Authorize]
    [HttpPost("like/{id}")]
    public async Task<ActionResult> LikeStatement(string id)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var user = await _authService.GetUserById(userId);
      if (user is null)
      {
        return Unauthorized();
      }
      if (_statementsRepo.GetStatementById(id) is null)
      {
        return ValidationProblem("Pareiškimas neegzistuoja");
      }
      var result = await _statementsRepo.LikeStatement(id, userId);
      if (result)
      {
        return Ok();
      }
      return ValidationProblem("Vartotojas jau yra paspaudęs like ant šio pasisakymo");
    }
    [Authorize]
    [HttpPost("unlike/{id}")]
    public async Task<ActionResult> UnlikeStatement(string id)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var user = await _authService.GetUserById(userId);
      if (user is null)
      {
        return Unauthorized();
      }
      var result = await _statementsRepo.UnlikeStatement(id, userId);
      if (result)
      {
        return Ok();
      }
      return ValidationProblem("Vartotojas nėra paspaudęs like ant šio pasisakymo");
    }
    [HttpGet("likeCount/{statementId}")]
    public async Task<ActionResult<int>> GetStatementLikeCount(string statementId)
    {
      var statement = await _statementsRepo.GetStatementEntityById(statementId);
      if (statement is null)
      {
        return ValidationProblem("Pasisakymas su šiuo ID neegzistuoja");
      }
      var count = _statementsRepo.GetLikeCount(statementId);

      return Ok(count);
    }
    [HttpGet("hasLiked/{statementId}")]
    public async Task<ActionResult<bool>> CheckIfCurrentUserHasLiked(string statementId)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var user = _authService.GetUserById(userId);
      if (user is null)
      {
        return Unauthorized();
      }
      var statement = _statementsRepo.GetStatementEntityById(statementId);
      if (statement is null)
      {
        return ValidationProblem("Pasisakymas su šiuo ID neegzistuoja");
      }
      var hasLiked = await _statementsRepo.CheckIfUserHasLiked(statementId, userId);
      return Ok(hasLiked);
    }
  }

  public class StatementsParams : PaginationParams
  {
    public string? Politician { get; set; }
    public List<string>? Tags { get; set; }
  }
}
