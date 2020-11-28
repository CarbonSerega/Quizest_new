using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models.Mongo
{
    public class QuizSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool ShowDuration { get; set; }

        public bool ShowComplexity { get; set; }

        public bool ShowCorrectAswersAfterPass { get; set; }

        public bool ShowParticipants { get; set; }

        public float? MarkScale { get; set; }
    }
}
