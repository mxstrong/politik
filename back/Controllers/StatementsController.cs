using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public async Task<ActionResult<List<StatementOutDto>>> GetAllStatements()
    {
      return Ok(await _statementsRepo.GetAllStatements());
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
  }
}
