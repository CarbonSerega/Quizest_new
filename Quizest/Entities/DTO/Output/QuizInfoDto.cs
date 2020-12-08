using System;

namespace Entities.DTO
{
    public class QuizInfoDto : DtoBase
    {
        public string Name { get; set; }

        public string QuizId { get; set; }

        public string Description { get; set; }

        public int? AmountOfPasses { get; set; }

        public int? AmountOfLikes { get; set; }

        public int AmountOfQuestions { get; set; }

        public string Complexity { get; set; }

        public string Duration { get; set; }

        public string PreviewBlobKey { get; set; }

        public bool? IsLiked { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public int? AttemptCount { get; set; }

        public OwnerShortInfoDto Owner { get; set; }
    }
}
