using System;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class QuizInfoUserConfiguration : IEntityTypeConfiguration<QuizInfoUser>
    {
        public void Configure(EntityTypeBuilder<QuizInfoUser> builder)
        {
            builder.HasData(
                new QuizInfoUser
                {
                    Id = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D431"),
                    UserId = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D429"),
                    QuizInfoId = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D430")
                }
            );
        }
    }
}
