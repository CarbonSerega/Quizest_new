using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Entities.Models.SQL
{
    [Table(nameof(QuizInfo))]
    public class QuizInfo : SqlEntityBase
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int? AmountOfPasses { get; set; }

        public int? AmountOfLikes { get; set; }

        public int AmountOfQuestions { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public Complexity? Complexity { get; set; }

        public TimeSpan Duration { get; set; }

        public bool? IsLiked { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        [JsonIgnore]
        public virtual User Owner { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Guid? OwnerId { get; set; }

        [JsonIgnore]
        public virtual ICollection<QuizInfoUser> QuizInfoUsers { get; set; }

        private IEnumerable<Guid> userIds;

        [NotMapped]
        public IEnumerable<Guid> UserIds
        {
            get => userIds ?? QuizInfoUsers.Select(qu => qu.UserId);
            set => userIds = value;
        }
    }
}
