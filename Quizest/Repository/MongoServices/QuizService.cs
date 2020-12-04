using MongoDB.Driver;
using Contracts;
using Contracts.Repos.Mongo;
using Entities.Models.Mongo;

namespace Repository.MongoServices
{
    public class QuizService : IMongoService
    {
        private readonly IMongoCollection<Quiz> quizzes;

        public QuizService(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            quizzes = database.GetCollection<Quiz>(settings.CollectionName);
        }

        public Quiz Get(string id) =>
             quizzes.Find(q => q.Id.Equals(id)).FirstOrDefault();
       

        public string Create(Quiz quiz = null)
        {
            if(quiz == null)
            {
                quiz = new Quiz();
            }

            quizzes.InsertOne(quiz);
            return quiz.Id;
        }

        public void Update(string id, Quiz quiz) =>
            quizzes.ReplaceOne(q => q.Id.Equals(id), quiz);

        public void Update(FilterDefinition<Quiz> filter, UpdateDefinition<Quiz> update) =>
            quizzes.UpdateOne(filter, update);

        public void Remove(string id) =>
            quizzes.DeleteOne(q => q.Id.Equals(id));
    }
}
