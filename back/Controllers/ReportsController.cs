using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using Politics.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Politics.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportsController : ControllerBase
  {
    private readonly IReportsRepository _reportsRepo;

    public ReportsController(IReportsRepository reportsRepo)
    {
      _reportsRepo = reportsRepo;
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ReportOutDto>>> GetReports(ReportsParams parameters)
    {
      var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
      if (role != "Admin")
      {
        return Unauthorized();
      }
      return Ok(await _reportsRepo.GetReports(parameters.PageNumber, parameters.PageSize, parameters.IsReviewed));
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ReportOutDto>> AddReport(ReportDto report)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      return await _reportsRepo.GenerateReport(report, userId);
    }

    [Authorize]
    [HttpPost("review/{reportId}")]
    public async Task<ActionResult> ReviewReport(string reportId)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
      if (role != "Admin")
      {
        return Unauthorized();
      }
      var result = await _reportsRepo.ReviewReport(reportId, userId);
      if (result)
      {
        return Ok();
      }
      return BadRequest("Operacijos atlikti nepavyko");
    }

    public class ReportsParams : PaginationParams
    {
      public bool IsReviewed { get; set; }
    }
  }
}
