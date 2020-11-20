using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SQL
{
    [Table(nameof(QuizInfoUser))]
    public class QuizInfoUser : SqlEntityBase
    {
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(QuizInfoId))]
        public Guid QuizInfoId { get; set; }

        public virtual QuizInfo QuizInfo { get; set; }
    }
}
