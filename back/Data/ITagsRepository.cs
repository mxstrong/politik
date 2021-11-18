using Politics.Dtos;
using Politics.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface ITagsRepository
  {
    Task<List<TagOutDto>> GetAllTags();
    Task<Tag> GetTagEntityById(string id);
    Task<TagOutDto> AddTag(TagDto tagDto, string userId);
    Task DeleteTag(string id);
  }
}
