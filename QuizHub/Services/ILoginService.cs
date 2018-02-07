using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Verifies a user's credentials and returns a JWT token for the user
        /// </summary>
        string Login(string Email, string Password);
    }
}
