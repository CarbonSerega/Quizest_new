using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Contracts;
using Entities;
using Entities.Models.SQL;
using System.Linq;

namespace Repository
{
    public class SQLRepositoryBase<T> : ISQLRepositoryBase<T> where T : SqlEntityBase
    {
        protected RepositoryContext repositoryContext;

        public SQLRepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => repositoryContext.Set<T>();

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression) => repositoryContext.Set<T>().Where(expression);

        public async Task<EntityEntry<T>> Create(T entity) => await repositoryContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    }
}
