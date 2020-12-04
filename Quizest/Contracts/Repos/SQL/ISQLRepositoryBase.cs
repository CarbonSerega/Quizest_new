using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contracts
{
    public interface ISQLRepositoryBase<T> where T : SqlEntityBase
    {
        Task<T> FindAll();
        Task<T> FindBy(Expression<Func<T, bool>> expression);
        Task<EntityEntry<T>> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
