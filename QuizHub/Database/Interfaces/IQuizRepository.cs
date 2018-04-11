using MongoDB.Bson;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database.Interfaces
{
    public interface IQuizRepository
    {
        Task<Quiz> CreateQuiz(string QuizName, ObjectId UserId);
        Task DeleteQuiz(ObjectId quizId);
        Task<Quiz> GetQuiz(ObjectId id);
        Task<Quiz> AddQuestion(ObjectId QuizId, string Question, string Answer);
        Task<Quiz> DeleteQuestion(string quizId, string questionId);
    }
}
