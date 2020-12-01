using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class QuizInfoUserRepository : SQLRepositoryBase<QuizInfoUser>, IQuizInfoUserRepository
    {
        public QuizInfoUserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
