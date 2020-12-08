using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contracts
{
    public interface ISQLRepositoryBase<T> where T : SqlEntityBase
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
        Task<EntityEntry<T>> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
