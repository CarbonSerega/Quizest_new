using System.Collections.Generic;

namespace Entities.Models.Mongo
{
    public class Answer : MongoEntityBase
    {
        /* EXAMPLE:
         * [{
         *   "Id": "answer_1"
         *   "ChosenAnswers": [{id: "choice_1"}, {id: "choice_3"]
         * }]
         * END
         */
        public ICollection<ChosenAnswer> ChosenAnswers { get; set; }
    }
}
