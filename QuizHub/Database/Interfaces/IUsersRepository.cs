using MongoDB.Bson;
using QuizHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database
{
    public interface IUsersRepository
    {
        bool UserExists(string Email);
        ObjectId RegisterUser(string Email, string HashedPassword, string Salt);
        User GetUser(string Email);
    }
}
