using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuizHub.Models.ConfigurationModels;
using QuizHub.Models.Database;

namespace QuizHub.Database
{
    public class QuizHubRepository : IGetMongoCollections
    {
        private MongoDbSettings _mongoDbSettings;
        private IMongoDatabase _database;

        public QuizHubRepository(IOptions<MongoDbSettings> mongoDbOptions)
        {
            _mongoDbSettings = mongoDbOptions.Value;
            var client = new MongoClient(_mongoDbSettings.ConnectionString);
            _database = client.GetDatabase("QuizHub");
        }

        public IMongoCollection<Quiz> GetQuizCollection()
        {
            return _database.GetCollection<Quiz>("Quizzes");
        }

        IMongoCollection<User> IGetMongoCollections.GetUsersCollection()
        {
            return _database.GetCollection<User>("Users");
        }
    }
}
