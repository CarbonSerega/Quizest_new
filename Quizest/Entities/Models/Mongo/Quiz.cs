using System.Collections.Generic;

namespace Entities.Models.Mongo
{
    public class Quiz : MongoEntityBase
    {
        public ICollection<Question> Questions { get; set; }

        public ICollection<Attempt> Attempts { get; set; }

        public QuizSettings QuizSettings { get; set; }
    }
}
