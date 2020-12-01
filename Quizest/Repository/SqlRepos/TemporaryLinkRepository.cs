using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class TemporaryLinkRepository : SQLRepositoryBase<TemporaryLink>, ITemporaryLinkRepository
    {
        public TemporaryLinkRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
    }
}
