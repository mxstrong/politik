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
        .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party.LongName))
        .ForMember(dest => dest.PartyShort, opt => opt.MapFrom(src => src.Party.ShortName))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + ' ' + src.LastName));
      CreateMap<PoliticianDto, Politician>();
      CreateMap<Party, PartyOutDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PartyId));
      CreateMap<PartyDto, Party>();
      CreateMap<TagDto, Tag>();
      CreateMap<Tag, TagOutDto>();
      CreateMap<TagOutDto, TagDto>();
      CreateMap<StatementDto, Statement>().ForMember(dest => dest.Tags, opt => opt.Ignore());
      CreateMap<Tag, string>().ConvertUsing(src => src.Name);
      CreateMap<Statement, StatementOutDto>()
        .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.DisplayName))
        .ForMember(dest => dest.Politician, opt => opt.MapFrom(src => src.Politician.FirstName + ' ' + src.Politician.LastName))
        .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
    }
  }
}
