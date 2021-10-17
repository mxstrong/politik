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
  public class PartiesController : ControllerBase
  {
    private readonly IPartiesRepository _repo;
    public PartiesController(IPartiesRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public async Task<ActionResult<List<PartyOutDto>>> GetAllParties()
    {
      var parties = await _repo.GetAllParties();
      return Ok(parties);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PartyOutDto>> GetPartyById(string id)
    {
      var party = await _repo.GetPartyById(id);
      if (party is null)
      {
        return ValidationProblem("Partija su tokiu ID neegzistuoja");
      }
      return Ok(party);
    }
    [HttpPost]
    public async Task<ActionResult<PartyOutDto>> AddParty(PartyDto partyDto)
    {
      var party = await _repo.AddParty(partyDto);
      if (party is null)
      {
        return ValidationProblem("Partijos pridėti nepavyko");
      }
      return Ok(party);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParty(string id)
    {
      var party = await _repo.DeleteParty(id);
      if (party is null)
      {
        return ValidationProblem("Partija nerasta");
      }
      return Ok(party);
    }
  }
}
