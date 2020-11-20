using System;
using System.Collections.Generic;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class QuizInfoConfiguration : IEntityTypeConfiguration<QuizInfo>
    {
        public void Configure(EntityTypeBuilder<QuizInfo> builder)
        {
            builder.HasData(
                new QuizInfo
                {
                    Id = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D430"),
                    Name = "Initial Quiz",
                    Description = "It is just an initial quiz for seeding",
                    AmountOfPasses = 0,
                    AmountOfLikes = 0,
                    AmountOfQuestions = 1,
                    Complexity = Complexity.EASY,
                    Duration = new TimeSpan(0, 1, 20),
                    IsLiked = false,
                    CreatedAt = new DateTime(2020, 11, 19, 12, 0, 0),
                    UpdatedAt = new DateTime(2020, 11, 20, 01, 0, 0),
                    ClosedAt = new DateTime(2020, 11, 20, 12, 0, 0),
                    OwnerId = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D428"),
                    UserIds = new List<Guid>
                    {
                        new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D429")
                    }
                }
            );
        }
    }
}
