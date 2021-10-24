using Politics.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface ITagsRepository
  {
    Task<List<TagOutDto>> GetAllTags();
    Task<TagOutDto> AddTag(TagDto tagDto);
    Task DeleteTag(string id);
  }
}
