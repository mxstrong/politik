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
    private readonly IStatementsRepository _repo;

    public StatementsController(IStatementsRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<StatementOutDto>>> GetAllStatements()
    {
      return Ok(await _repo.GetAllStatements());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<StatementOutDto>> GetStatetementById(string id)
    {
      return Ok(await _repo.GetStatementById(id));
    }
    [HttpPost]
    public async Task<ActionResult<StatementOutDto>> AddStatement(StatementDto statementDto)
    {
      var createdStatement = await _repo.AddStatement(statementDto);
      return CreatedAtAction(nameof(GetStatetementById), new { id = createdStatement.StatementId }, createdStatement);
    }
  }
}
