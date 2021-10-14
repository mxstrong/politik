using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<ActionResult<List<PoliticianDto>>> GetAllPoliticians()
    {
      var politicians = await _repo.GetAllPoliticians();
      return Ok(politicians);
    }
    [HttpPost]
    public async Task<ActionResult> AddPolitician(PoliticianDto politicianDto)
    {
      var createdPolitician = await _repo.AddPolitician(politicianDto);
      return Ok(createdPolitician);
    }
  }
}
