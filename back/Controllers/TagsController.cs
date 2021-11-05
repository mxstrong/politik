using Microsoft.AspNetCore.Mvc;
using Politics.Data;
using Politics.Dtos;
using System.Collections.Generic;
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

    [HttpPost]
    public async Task<ActionResult<TagOutDto>> AddTag(TagDto tagDto)
    {
      return Ok(await _repo.AddTag(tagDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveTag(string id)
    {
      await _repo.DeleteTag(id);
      return NoContent();
    }
  }
}
