using MongoDB.Bson;
using MongoDB.Driver;
using QuizHub.Models;
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

        public ObjectId RegisterUser(string Email, string HashedPassword, string Salt)
        {
            var user = new User()
            {
                Email = Email,
                Password = HashedPassword,
                Salt = Salt
            };

            _userCollection.InsertOne(user);

            return user._id;
        }

        public bool UserExists(string Email)
        {
            var filter = Builders<User>.Filter.Eq("Email", Email);
            var document = _userCollection.Find(filter).FirstOrDefault();

            return document != null;
        }

        public User GetUser(string Email)
        {
            var filter = Builders<User>.Filter.Eq("Email", Email);
            var user = _userCollection.Find(filter).FirstOrDefault();

            return user;
        }
    }
}
