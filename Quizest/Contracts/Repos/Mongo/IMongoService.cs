using Entities.Models.Mongo;
using MongoDB.Driver;

namespace Contracts.Repos.Mongo
{
    public interface IMongoService
    {
        Quiz Get(string id);
        string Create(string id, Quiz quiz = null);
        void Update(string id, Quiz entity);
        void Update(FilterDefinition<Quiz> filter, UpdateDefinition<Quiz> update);
        void Remove(string id);
    }
}
