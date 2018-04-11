using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using QuizHub.Database.Interfaces;
using QuizHub.Models.Database;

namespace QuizHub.Services
{
    public class QuizService : IQuizService
    {
        private IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Quiz> CreateQuiz(string Name, ObjectId UserName)
        {
            return await _quizRepository.CreateQuiz(Name, UserName);
        }

        public async void DeleteQuiz(ObjectId quizId)
        {
            await _quizRepository.DeleteQuiz(quizId);
        }

        public Task<Quiz> GetQuiz(ObjectId id)
        {
            return _quizRepository.GetQuiz(id);
        }

        public async Task<bool> IsOwner(ObjectId UserId, ObjectId QuizId)
        {
            var quiz = await _quizRepository.GetQuiz(QuizId);
            return quiz.OwnerId == UserId;
        }

        public async Task<Quiz> AddQuestion(ObjectId QuizId, string Question, string Answer)
        {
            var quiz = await _quizRepository.AddQuestion(QuizId, Question, Answer);
            return quiz;
        }

        public async Task<Quiz> DeleteQuizAsync(string quizId, string questionId)
        {
            var quiz = await _quizRepository.DeleteQuestion(quizId, questionId);
            return quiz;
        }
    }
}
