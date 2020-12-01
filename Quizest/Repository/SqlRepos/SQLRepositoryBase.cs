using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository
{
    public abstract class SQLRepositoryBase<T> : ISQLRepositoryBase<T> where T : SqlEntityBase
    {
        protected RepositoryContext repositoryContext;

        protected SQLRepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
            => !trackChanges
               ? repositoryContext.Set<T>().AsNoTracking()
               : repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
            => !trackChanges
               ? repositoryContext.Set<T>().Where(expression).AsNoTracking()
               : repositoryContext.Set<T>().Where(expression);

        public async Task<EntityEntry<T>> Create(T entity) => await repositoryContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    }
}
