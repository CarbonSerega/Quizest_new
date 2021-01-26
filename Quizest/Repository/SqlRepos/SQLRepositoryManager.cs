using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class SQLRepositoryManager : ISQLRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;

        public SQLRepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public ISQLRepositoryBase<T> Repository<T>() where T : SqlEntityBase => 
            new SQLRepositoryBase<T>(repositoryContext);
        
        public void Save() => repositoryContext.SaveChanges();

        public void Detach() => repositoryContext.ChangeTracker.Clear();
    }
}
