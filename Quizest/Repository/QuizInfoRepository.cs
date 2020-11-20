using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class QuizInfoRepository : SQLRepositoryBase<QuizInfo>, IQuizInfoRepository
    {
        public QuizInfoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
