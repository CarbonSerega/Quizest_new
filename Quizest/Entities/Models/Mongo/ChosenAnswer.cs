using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models.Mongo
{
    public class ChosenAnswer : MongoEntityBase
    {
        [BsonRequired]
        public string ChoiceId { get; set; }

        public string Text { get; set; }
    }
}
