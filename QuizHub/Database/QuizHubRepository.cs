using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuizHub.Models;
using QuizHub.Models.ConfigurationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        IMongoCollection<User> IGetMongoCollections.GetUsersCollection()
        {
            return _database.GetCollection<User>("Users");
        }
    }
}
