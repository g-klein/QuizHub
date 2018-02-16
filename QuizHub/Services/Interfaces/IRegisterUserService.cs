using MongoDB.Bson;
using QuizHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Services
{
    public interface IRegisterUserService
    {
        Task<ObjectId> RegisterUser(string Email, string Password);
    }
}
