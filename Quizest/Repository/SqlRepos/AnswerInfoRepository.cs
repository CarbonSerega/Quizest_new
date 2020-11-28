using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class AnswerInfoRepository : SQLRepositoryBase<AnswerInfo>, IAnswerInfoRepository
    {
        public AnswerInfoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
