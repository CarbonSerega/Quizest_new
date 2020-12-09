namespace Entities.Models.Mongo
{
    public class QuizSettings : MongoEntityBase
    {
        public bool ShowDuration { get; set; }

        public bool ShowComplexity { get; set; }

        public bool ShowCorrectAswersAfterPass { get; set; }

        public bool ShowParticipants { get; set; }

        public float? MarkScale { get; set; }
    }
}
