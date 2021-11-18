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
  public class TagsController : ControllerBase
  {
    private readonly ITagsRepository _repo;

    public TagsController(ITagsRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagOutDto>>> GetAllTags()
    {
      return Ok(await _repo.GetAllTags());
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TagOutDto>> AddTag(TagDto tagDto)
    {
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      return Ok(await _repo.AddTag(tagDto, userId));
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveTag(string id)
    {
      var tagToDelete = await _repo.GetTagEntityById(id);
      var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
      var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (role != "Admin" && userId != tagToDelete.CreatedById)
      {
        return Unauthorized();
      }
      await _repo.DeleteTag(id);
      return NoContent();
    }
  }
}
