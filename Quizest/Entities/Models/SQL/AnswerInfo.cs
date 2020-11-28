using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models.SQL
{
    [Table(nameof(AnswerInfo))]
    public class AnswerInfo : SqlEntityBase
    {
        [JsonIgnore]
        public string AttemptId { get; set; }

        public int AmountOfCorrectQuestions { get; set; }

        public TimeSpan? SpentTime { get; set; }

        public float? Mark { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual QuizInfo QuizInfo { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(QuizInfoId))]
        public Guid? QuizInfoId { get; set; }
    }
}
