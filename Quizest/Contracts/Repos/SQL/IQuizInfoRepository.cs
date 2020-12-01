using System.Collections.Generic;
using Entities.Models.SQL;

namespace Contracts
{
    public interface IQuizInfoRepository
    {
        IEnumerable<QuizInfo> GetAllQuizInfos(bool trackChanges = true);
        QuizInfo GetQuizInfo(System.Guid id, bool trackChanges = true);
    }
}
