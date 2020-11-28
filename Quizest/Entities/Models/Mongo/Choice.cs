using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models.Mongo
{
    public class Choice : MongoEntityBase
    {
        [BsonRequired]
        public string Text { get; set; }

        [BsonRequired]
        public bool? IsCorrect { get; set; }

        //Correct variants of a text answer on a question
        public IList<string> CorrectTextAnswers { get; set; }

        public string ImagePath { get; set; }

        public float? Mark { get; set; }
    }
}
