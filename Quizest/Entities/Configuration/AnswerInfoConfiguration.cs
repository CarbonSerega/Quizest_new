using System;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class AnswerInfoConfiguration : IEntityTypeConfiguration<AnswerInfo>
    {
        public void Configure(EntityTypeBuilder<AnswerInfo> builder)
        {
            builder.HasData(
               new AnswerInfo
               {
                   Id = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D432"),
                   AmountOfCorrectQuestions = 1,
                   SpentTime = new TimeSpan(1, 10, 0),
                   Mark = 5.0F,
                   UserId = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D429"),
                   QuizInfoId = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D430")
               }
            );
        }
    }
}
