using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Models.Auth
{
    public class PasswordHashResult
    {
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }
}
