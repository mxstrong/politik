using AutoMapper;
using Politics.Dtos;
using Politics.Model;

namespace Politics.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Politician, PoliticianOutDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PoliticianId))
        .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party.ShortName))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + ' ' + src.LastName));
      CreateMap<PoliticianDto, Politician>();
      CreateMap<Party, PartyOutDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PartyId));
      CreateMap<PartyDto, Party>();
    }
  }
}
