using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using System.Threading.Tasks;
using Politics.Dtos;
using Politics.Helpers;
using System.Text.Json;

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
    public async Task<ActionResult<PaginatedList<PoliticianOutDto>>> GetAllPoliticians([FromQuery] PoliticiansParams parameters)
    {
      var politicians = await _repo.GetAllPoliticians(parameters.PageNumber, parameters.PageSize);
      var paginationMetadata = new
      {
        politicians.Count,
        politicians.PageSize,
        politicians.PageIndex,
        politicians.TotalPages,
        politicians.HasNextPage,
        politicians.HasPreviousPage
      };
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

      return Ok(politicians);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PoliticianOutDto>> GetPoliticianById(string id)
    {
      return await _repo.GetPoliticianById(id);
    }
    [HttpPost]
    public async Task<ActionResult> AddPolitician(PoliticianDto politicianDto)
    {
      if (politicianDto.FirstName is null || politicianDto.FirstName.Length < 1) 
      {
        return ValidationProblem("Nenurodėte politiko vardo");
      }
      if (politicianDto.FirstName is null || politicianDto.FirstName.Length > 50)
      {
        return ValidationProblem("Nurodytas politiko vardas per ilgas (maksimalus ilgis 50 simbolių)");
      }
      if (politicianDto.LastName is null || politicianDto.LastName.Length < 1)
      {
        return ValidationProblem("Nenurodėte politiko pavardės");
      }
      if (politicianDto.LastName is null || politicianDto.LastName.Length > 50)
      {
        return ValidationProblem("Nenurodyta politiko pavardė per ilga (maksimalus ilgis 50 simbolių)");
      }
      if (politicianDto.Description is null || politicianDto.Description.Length < 1)
      {
        return ValidationProblem("Nepateikėte politiko aprašymo");
      }
      if (politicianDto.Description is null || politicianDto.Description.Length > 250)
      {
        return ValidationProblem("Nurodytas politiko aprašymas per ilgas (maksimalus ilgis 250 simbolių)");
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

  public class PoliticiansParams : PaginationParams { }
}
