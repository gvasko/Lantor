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
            CreateMap<MultilingualSample, EmptyMultilingualSampleDTO>();
            CreateMap<LanguageSample, EmptyLanguageSampleDTO>();
            CreateMap<LanguageSample, LanguageSampleDTO>();
        }
    }
}
