using QuizHub.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Auth
{
    public interface IHashPasswords
    {
        PasswordHashResult HashPassword(string Password);
    }
}
