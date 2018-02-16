using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Models.Auth
{
    public class AuthJwtPayload
    {
        public ObjectId UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
