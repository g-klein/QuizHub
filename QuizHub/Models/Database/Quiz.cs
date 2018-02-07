using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Models.Database
{
    public class Quiz
    {
        ObjectId _id { get; set; }
        public ObjectId OwnerId { get; set; }
        public string Name { get; set; }
    }
}
