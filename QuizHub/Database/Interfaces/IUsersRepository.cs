using MongoDB.Bson;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database
{
    public interface IUsersRepository
    {
        Task<bool> UserExists(string Email);
        Task<ObjectId> RegisterUser(string Email, string HashedPassword, string Salt);
        Task<User> GetUser(string Email);
    }
}
