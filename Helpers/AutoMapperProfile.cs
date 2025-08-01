using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Models;

namespace abaBackOffice.Helpers
{
    public class AbaAutoMapperProfile : Profile
    {
        public AbaAutoMapperProfile()
        {
            
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<OtpCode, OtpCodeDto>().ReverseMap();

            
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();

            
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentDto>().ReverseMap();
            CreateMap<ReinforcementProgram, ReinforcementProgramDto>().ReverseMap();
            CreateMap<ReinforcerAgent, ReinforcerAgentDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            
            CreateMap<Domain, DomainDto>().ReverseMap();

            CreateMap<AbllsTask, AbllsTaskDto>()
                .ForMember(dest => dest.EvaluationCriterias, opt => opt.MapFrom(src => src.EvaluationCriterias))
                .ForMember(dest => dest.MaterialPhotos, opt => opt.MapFrom(src => src.MaterialPhotos))
                .ForMember(dest => dest.BaselineContents, opt => opt.MapFrom(src => src.BaselineContents))
                .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
                .ReverseMap()
                .ForMember(dest => dest.EvaluationCriterias, opt => opt.Ignore())
                .ForMember(dest => dest.MaterialPhotos, opt => opt.Ignore())
                .ForMember(dest => dest.BaselineContents, opt => opt.Ignore())
                .ForMember(dest => dest.Domain, opt => opt.Ignore());


            CreateMap<EvaluationCriteria, EvaluationCriteriaDto>()
                .ForMember(dest => dest.MaterialPhotoIds, opt => opt.MapFrom(src =>
                    src.EvaluationCriteriaMaterials.Select(x => x.MaterialPhotoId)))
                .ReverseMap()
                .ForMember(dest => dest.EvaluationCriteriaMaterials, opt => opt.Ignore());

            CreateMap<MaterialPhoto, MaterialPhotoDto>().ReverseMap();
            CreateMap<BaselineContent, BaselineContentDto>().ReverseMap();
            CreateMap<EvaluationCriteriaMaterialDto, EvaluationCriteriaMaterial>().ReverseMap();


           
        }
    }
}
