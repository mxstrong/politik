using AutoMapper;
using Politics.Data;
using Politics.Model;

namespace Politics.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Politician, PoliticianDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PoliticianId))
        .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party.Name));
      CreateMap<PoliticianDto, Politician>();
    }
  }
}
