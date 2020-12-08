using System;
using Entities.Models.SQL;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO
{
    public class QuizInfoForCreationDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Complexity Complexity { get; set; }

        public int? AttemptCount { get; set; }

        public Guid OwnerId { get; set; }

        public DateTime? ClosedAt { get; set; }

        public int? Duration { get; set; }

        public IFormFile PreviewImage { get; set; }
    }
}
