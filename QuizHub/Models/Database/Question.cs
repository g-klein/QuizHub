using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Models.Database
{
    public class Question
    {
        public string _id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
