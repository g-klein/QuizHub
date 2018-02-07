using QuizHub.Auth;
using QuizHub.Database;
using QuizHub.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Services
{
    public class LoginService : ILoginService
    {
        private IUsersRepository _usersRepository;
        private IVerifyPasswords _verifyPasswords;
        private IGenerateJwts _generateJwts;

        public LoginService(IUsersRepository usersRepository, IVerifyPasswords verifyPasswords, IGenerateJwts generateJwts)
        {
            _usersRepository = usersRepository;
            _verifyPasswords = verifyPasswords;
            _generateJwts = generateJwts;
        }

        public string Login(string Email, string Password)
        {
            var user = _usersRepository.GetUser(Email);
            if (user != null)
            {
                var isValid = _verifyPasswords.IsPasswordValid(Password, user.Salt, user.Password);
                if (isValid)
                {
                    return _generateJwts.GetJwtString(user._id.ToString(), user.Email);
                }
            }

            throw new LoginFailedException();
        }
    }
}
