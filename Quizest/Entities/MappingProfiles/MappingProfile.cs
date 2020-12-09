using AutoMapper;
using Entities.DTO;
using Entities.Models.SQL;
using Utility;

namespace Entities.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var map = CreateMap<QuizInfo, QuizInfoDto>()

                .ForMember(q => q.Owner, opt => opt.MapFrom(o => CreateOwnerShortInfoDto(o)))

                .ForMember(q => q.Complexity, opt => opt.MapFrom(o =>
                    o.Complexity == null ? string.Empty : o.Complexity.Value.ToString()))

                .ForMember(q => q.Duration, opt => opt.MapFrom(o => DateTimeUtils.ToDuration(o.Duration)))

                .ForMember(q => q.PreviewBlobKey, opt => opt.MapFrom(o => FileUtils.GetContent(o.PreviewPath)));

            
            _ = CreateMap<QuizInfo, QuizInfoForOwnerDto>()

                .ForMember(q => q.Owner, opt => opt.MapFrom(o => CreateOwnerShortInfoDto(o)))

                .ForMember(q => q.Complexity, opt => opt.MapFrom(o =>
                    o.Complexity == null ? string.Empty : o.Complexity.Value.ToString()))

                .ForMember(q => q.Duration, opt => opt.MapFrom(o => DateTimeUtils.ToDuration(o.Duration)))

                .ForMember(q => q.PreviewBlobKey, opt => opt.MapFrom(o => FileUtils.GetContent(o.PreviewPath)));


            _ = CreateMap<QuizInfoForCreationDto, QuizInfo>();
        }

        private OwnerShortInfoDto CreateOwnerShortInfoDto(QuizInfo quizInfo)
            => new OwnerShortInfoDto
            {
                Id = quizInfo.Owner.Id.ToString(),
                FirstName = quizInfo.Owner.FirstName,
                LastName = quizInfo.Owner.LastName,
                AvatarBlobKey = FileUtils.GetContent(quizInfo.Owner.AvatarPath)
            };
    }
}
