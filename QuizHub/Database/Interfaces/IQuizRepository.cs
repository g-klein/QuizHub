using MongoDB.Bson;
using QuizHub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Database.Interfaces
{
    interface IQuizRepository
    {
        List<Quiz> GetQuizes(ObjectId customerId);
        void RenameQuiz(ObjectId QuizId, string QuizName);
        //TODO: finish doing this
    }
}
