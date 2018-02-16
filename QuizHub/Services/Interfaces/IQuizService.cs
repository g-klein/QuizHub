using MongoDB.Bson;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Services
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuiz(string Name, ObjectId UserName);
        void DeleteQuiz(ObjectId quizId);
        Task<Quiz> GetQuiz(ObjectId id);
        Task<bool> IsOwner(ObjectId UserId, ObjectId QuizId);
        Task<Quiz> AddQuestion(ObjectId quizId, string Question, string Answer);
    }
}
