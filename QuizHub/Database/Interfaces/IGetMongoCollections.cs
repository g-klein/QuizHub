using MongoDB.Driver;
using QuizHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database
{
    public interface IGetMongoCollections
    {
        IMongoCollection<User> GetUsersCollection();
    }
}
