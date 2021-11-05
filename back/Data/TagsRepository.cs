using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public class TagsRepository : ITagsRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public TagsRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<TagOutDto> AddTag(TagDto tagDto)
    {
      var existingTag = await _context.Tags.FirstOrDefaultAsync(tag => tag.Name == tagDto.Name);
      if (existingTag is not null)
      {
        return _mapper.Map<Tag, TagOutDto>(existingTag);
      }
      var tag = _mapper.Map<TagDto, Tag>(tagDto);
      tag.TagId = Guid.NewGuid().ToString();
      tag.CreatedAt = DateTime.Now;
      tag.CreatedById = "test";
      await _context.AddAsync(tag);
      await _context.SaveChangesAsync();
      return _mapper.Map<Tag, TagOutDto>(tag);
    }

    public async Task DeleteTag(string id)
    {
      var tag = _context.Tags.Find(id);
      _context.Tags.Remove(tag);
      await _context.SaveChangesAsync();
    }

    public async Task<List<TagOutDto>> GetAllTags()
    {
      var tags = await _context.Tags.ToListAsync();
      return _mapper.Map<List<Tag>, List<TagOutDto>>(tags);
    }
  }
}
