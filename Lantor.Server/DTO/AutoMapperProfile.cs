using AutoMapper;
using Lantor.DomainModel;

namespace Lantor.Server.DTO
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MultilingualSample, MultilingualSampleListInfoDTO>()
                .ForMember(dest => dest.LanguageCount, opt => opt.MapFrom(src => src.Languages.Count));
            CreateMap<MultilingualSampleListInfoDTO, MultilingualSample>();
            CreateMap<MultilingualSample, EmptyMultilingualSampleDTO>().ReverseMap();
            CreateMap<LanguageSample, EmptyLanguageSampleDTO>().ReverseMap();
            CreateMap<LanguageSample, LanguageSampleDTO>().ReverseMap();
            CreateMap<Alphabet, AlphabetListInfoDTO>().ForMember(dest => dest.Dim, opt => opt.MapFrom(src => src.Dim));
            CreateMap<AlphabetListInfoDTO, Alphabet>();
        }
    }
}
