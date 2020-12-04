using System;
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

                .ForMember(q => q.Owner, opt => opt.MapFrom(o => new OwnerShortInfoDto
                {
                    Id = o.Owner.Id.ToString(),
                    FullName = $"{o.Owner.FirstName} {o.Owner.LastName}",
                    AvatarBlobKey = FileUtils.GetContent(o.Owner.AvatarPath)
                }))

                .ForMember(q => q.Complexity, opt => opt.MapFrom(o =>
                    o.Complexity == null ? string.Empty : o.Complexity.Value.ToString()))

                .ForMember(q => q.Duration, opt => opt.MapFrom(o =>
                    o.Duration == null 
                    ? string.Empty 
                    : TimeSpan.FromSeconds(o.Duration.Value).Hours == 0 
                      ? TimeSpan.FromSeconds(o.Duration.Value).ToString(@"mm\:ss")
                      : TimeSpan.FromSeconds(o.Duration.Value).ToString(@"hh\:mm\:ss")));

            _ = CreateMap<QuizInfo, QuizInfoForOwnerDto>()
                .ForMember(q => q.Owner, opt => opt.MapFrom(o => new OwnerShortInfoDto
                {
                    Id = o.Owner.Id.ToString(),
                    FullName = $"{o.Owner.FirstName} {o.Owner.LastName}",
                    AvatarBlobKey = FileUtils.GetContent(o.Owner.AvatarPath)
                }))

                .ForMember(q => q.Complexity, opt => opt.MapFrom(o =>
                    o.Complexity == null ? string.Empty : o.Complexity.Value.ToString()))

                .ForMember(q => q.Duration, opt => opt.MapFrom(o =>
                    o.Duration == null
                    ? string.Empty
                    : TimeSpan.FromSeconds(o.Duration.Value).Hours == 0
                      ? TimeSpan.FromSeconds(o.Duration.Value).ToString(@"mm\:ss")
                      : TimeSpan.FromSeconds(o.Duration.Value).ToString(@"hh\:mm\:ss")))

                .ForMember(q => q.TemporaryLink, opt => opt.MapFrom(o => o.TemporaryLink.Link));


            _ = CreateMap<QuizInfoForCreationDto, QuizInfo>();
        }
    }
}
