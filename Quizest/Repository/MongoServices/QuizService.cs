using System.Threading.Tasks;
using MongoDB.Driver;
using Contracts;
using Contracts.Repos.Mongo;
using Entities.Models.Mongo;

namespace Repository.MongoServices
{
    public class QuizService : IMongoServiceBase
    {
        private readonly IMongoCollection<Quiz> quizzes;

        public QuizService(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            quizzes = database.GetCollection<Quiz>(settings.CollectionName);
        }

        public async Task<Quiz> Get(string id) =>
            await quizzes.FindAsync(q => q.Id.Equals(id)).Result.FirstOrDefaultAsync();
       

        public async Task<string> Create(Quiz quiz)
        {
            await quizzes.InsertOneAsync(quiz);
            return quiz.Id;
        }

        public async void Update(string id, Quiz quiz) =>
            await quizzes.ReplaceOneAsync(q => q.Id.Equals(id), quiz);

        public async void Update(FilterDefinition<Quiz> filter, UpdateDefinition<Quiz> update) =>
            await quizzes.UpdateOneAsync(filter, update);

        public async void Remove(string id) =>
            await quizzes.DeleteOneAsync(q => q.Id.Equals(id));
    }
}
