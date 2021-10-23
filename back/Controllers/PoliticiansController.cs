using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Politics.Dtos;

namespace Politics.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PoliticiansController : ControllerBase
  {
    private readonly IPoliticiansRepository _repo;

    public PoliticiansController(IPoliticiansRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public async Task<ActionResult<List<PoliticianOutDto>>> GetAllPoliticians()
    {
      var politicians = await _repo.GetAllPoliticians();
      return Ok(politicians);
    }
    [HttpPost]
    public async Task<ActionResult> AddPolitician(PoliticianDto politicianDto)
    {
      if (politicianDto.FirstName is null) 
      {
        return ValidationProblem("Nenurodėte politiko vardo");
      } 
      if (politicianDto.LastName is null)
      {
        return ValidationProblem("Nenurodėte politiko pavardės");
      }
      var createdPolitician = await _repo.AddPolitician(politicianDto);
      if (createdPolitician is null)
      {
        return ValidationProblem("Politiko pridėti nepavyko");
      }
      return Ok(createdPolitician);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePolitician(string id)
    {
      var politician = await _repo.DeletePolitician(id);
      if (politician is null)
      {
        return ValidationProblem("Politikas nerastas");
      }
      return NoContent();
    }
  }
}
