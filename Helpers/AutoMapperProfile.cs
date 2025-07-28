using AutoMapper;
using abaBackOffice.DTOs;
using abaBackOffice.Models;

namespace abaBackOffice.Helpers
{
    public class AbaAutoMapperProfile : Profile
    {
        public AbaAutoMapperProfile()
        {
            // 🔐 Auth / Users
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<OtpCode, OtpCodeDto>().ReverseMap();

            // 📄 Documents / Vidéos
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();

            // 📚 Blog / Renforcement
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentDto>().ReverseMap();
            CreateMap<ReinforcementProgram, ReinforcementProgramDto>().ReverseMap();
            CreateMap<ReinforcerAgent, ReinforcerAgentDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            // 🧠 ABLLS TASK
            CreateMap<AbllsTask, AbllsTaskDto>()
                .ForMember(dest => dest.EvaluationCriterias, opt => opt.MapFrom(src => src.EvaluationCriterias))
                .ForMember(dest => dest.MaterialPhotos, opt => opt.MapFrom(src => src.MaterialPhotos))
                .ForMember(dest => dest.BaselineContents, opt => opt.MapFrom(src => src.BaselineContents))
                .ReverseMap();

            CreateMap<EvaluationCriteria, EvaluationCriteriaDto>()
                .ForMember(dest => dest.MaterialPhotoIds, opt => opt.MapFrom(src =>
                    src.EvaluationCriteriaMaterials.Select(x => x.MaterialPhotoId)))
                .ReverseMap()
                .ForMember(dest => dest.EvaluationCriteriaMaterials, opt => opt.Ignore());

            CreateMap<MaterialPhoto, MaterialPhotoDto>().ReverseMap();
            CreateMap<BaselineContent, BaselineContentDto>().ReverseMap();
            CreateMap<EvaluationCriteriaMaterialDto, EvaluationCriteriaMaterial>().ReverseMap();


            // 🗑️ Supprimé : AbllsVideo (non utilisé dans nouvelle logique)
            // CreateMap<AbllsVideo, AbllsVideoDto>().ReverseMap();
        }
    }
}
