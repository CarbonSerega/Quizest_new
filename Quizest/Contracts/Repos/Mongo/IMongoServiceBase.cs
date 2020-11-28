using System.Threading.Tasks;
using Entities.Models.Mongo;
using MongoDB.Driver;

namespace Contracts.Repos.Mongo
{
    public interface IMongoServiceBase
    {
        Task<Quiz> Get(string id);
        Task<string> Create(Quiz quiz);
        void Update(string id, Quiz entity);
        void Update(FilterDefinition<Quiz> filter, UpdateDefinition<Quiz> update);
        void Remove(string id);
    }
}
