// AutoMapper profile for ABA models
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
            CreateMap<AbllsTask, AbllsTaskDto>().ReverseMap();
            CreateMap<AbllsVideo, AbllsVideoDto>().ReverseMap();
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentDto>().ReverseMap();
            CreateMap<ReinforcementProgram, ReinforcementProgramDto>().ReverseMap();
            CreateMap<ReinforcerAgent, ReinforcerAgentDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}
