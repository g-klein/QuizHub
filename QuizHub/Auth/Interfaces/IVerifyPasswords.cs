using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Auth
{
    public interface IVerifyPasswords
    {
        bool IsPasswordValid(string password, string salt, string hash);
    }
}
