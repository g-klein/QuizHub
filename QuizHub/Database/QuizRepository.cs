using MongoDB.Bson;
using MongoDB.Driver;
using QuizHub.Database.Interfaces;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database
{
    public class QuizRepository : IQuizRepository
    {
        private IMongoCollection<Quiz> _quizCollection;

        public QuizRepository(IGetMongoCollections getMongoCollections)
        {
            _quizCollection = getMongoCollections.GetQuizCollection();
        }

        public async Task<Quiz> CreateQuiz(string QuizName, ObjectId UserId)
        {
            var quiz = new Quiz() { Name = QuizName, OwnerId = UserId, Questions = new Question[] { } };
            await _quizCollection.InsertOneAsync(quiz);

            return quiz;
        }

        public async Task DeleteQuiz(ObjectId quizId)
        {
            var filter = Builders<Quiz>.Filter.Eq("_id", quizId);
            await _quizCollection.DeleteOneAsync(filter);
        }

        public async Task<Quiz> GetQuiz(ObjectId id)
        {
            var filter = Builders<Quiz>.Filter.Eq("_id", id);
            var quiz = await _quizCollection.FindAsync<Quiz>(filter);
            return quiz.FirstOrDefault();
        }

        public async Task<Quiz> AddQuestion(ObjectId QuizId, string Question, string Answer)
        {
            var filter = Builders<Quiz>.Filter.Eq("_id", QuizId);

            var update = Builders<Quiz>.Update
                            .Push(e => e.Questions, new Question(){QuestionText = Question, AnswerText = Answer, _id = Guid.NewGuid().ToString()});

            var options = new FindOneAndUpdateOptions<Quiz>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _quizCollection.FindOneAndUpdateAsync(filter, update, options);
        }
    }
}
