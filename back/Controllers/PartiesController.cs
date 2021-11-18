using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using System.Collections.Generic;
using System.Security.Claims;
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
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PartyOutDto>> AddParty(PartyDto partyDto)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var party = await _repo.AddParty(partyDto, userId);
      if (party is null)
      {
        return ValidationProblem("Partijos pridėti nepavyko");
      }
      return Ok(party);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParty(string id)
    {
      var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
      if (role != "Admin")
      {
        return Unauthorized();
      }
      var party = await _repo.DeleteParty(id);
      if (party is null)
      {
        return ValidationProblem("Partija nerasta");
      }
      return NoContent();
    }
  }
}
