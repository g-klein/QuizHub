using MongoDB.Bson;
using MongoDB.Driver;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database
{
    public class UsersRepository : IUsersRepository
    {
        private IMongoCollection<User> _userCollection;

        public UsersRepository(IGetMongoCollections getMongoCollections)
        {
            _userCollection = getMongoCollections.GetUsersCollection();
        }

        public async Task<ObjectId> RegisterUser(string Email, string HashedPassword, string Salt)
        {
            var user = new User()
            {
                Email = Email,
                Password = HashedPassword,
                Salt = Salt
            };

            await _userCollection.InsertOneAsync(user);

            return user._id;
        }

        public async Task<bool> UserExists(string Email)
        {
            var filter = Builders<User>.Filter.Eq("Email", Email);
            var document = await _userCollection.FindAsync(filter);

            return document.FirstOrDefault() != null;
        }

        public async Task<User> GetUser(string Email)
        {
            var filter = Builders<User>.Filter.Eq("Email", Email);
            var user = await _userCollection.FindAsync(filter);

            return user.FirstOrDefault();
        }
    }
}
