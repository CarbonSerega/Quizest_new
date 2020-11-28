using System.Collections.Generic;

namespace Entities.Models.Mongo
{
    public class Attempt : MongoEntityBase
    {
        public ICollection<Answer> Answers { get; set; }
    }
}
