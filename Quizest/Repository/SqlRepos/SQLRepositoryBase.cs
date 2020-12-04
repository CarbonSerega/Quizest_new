using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Contracts;
using Entities;
using Entities.Models.SQL;

namespace Repository
{
    public class SQLRepositoryBase<T> : ISQLRepositoryBase<T> where T : SqlEntityBase
    {
        protected RepositoryContext repositoryContext;

        public SQLRepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<T> FindAll()
            => await repositoryContext.Set<T>().FindAsync();

        public async Task<T> FindBy(Expression<Func<T, bool>> expression)
            => await repositoryContext.Set<T>().FindAsync(expression);

        public async Task<EntityEntry<T>> Create(T entity) 
            => await repositoryContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    }
}
