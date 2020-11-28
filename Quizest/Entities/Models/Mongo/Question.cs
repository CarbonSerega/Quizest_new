using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Entities.Models.Mongo
{
    public class Question : MongoEntityBase
    {
        [BsonRequired]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public QuestionType Type { get; set; }

        public ICollection<Choice> Choices { get; set; } 
    }
}
