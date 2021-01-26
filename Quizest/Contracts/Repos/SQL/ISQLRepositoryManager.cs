using Entities.Models.SQL;

namespace Contracts
{
    public interface ISQLRepositoryManager
    {
        ISQLRepositoryBase<T> Repository<T>() where T : SqlEntityBase;

        void Save();

        void Detach();
    }
}
