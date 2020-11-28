using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models.Mongo
{
    public abstract class MongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
