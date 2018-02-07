using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Models
{
    public class User
    {
        public ObjectId _id;
        public string Email;
        public string Salt;
        public string Password;
    }
}
