using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class UserRepository : SQLRepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
