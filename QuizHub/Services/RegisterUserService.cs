using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using QuizHub.Auth;
using QuizHub.Database;
using QuizHub.Models;
using QuizHub.Models.Exceptions;

namespace QuizHub.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private IUsersRepository _usersRepository;
        private IHashPasswords _hashPasswords;

        public RegisterUserService(IUsersRepository usersRepository, IHashPasswords hashPasswords)
        {
            _usersRepository = usersRepository;
            _hashPasswords = hashPasswords;
        }

        public ObjectId RegisterUser(string Email, string Password)
        {
            if (_usersRepository.UserExists(Email))
            {
                throw new DuplicateUserException();
            } else
            {
                var encryptionResult = _hashPasswords.HashPassword(Password);
                return _usersRepository.RegisterUser(Email, encryptionResult.HashedPassword, encryptionResult.Salt);
            }
        }
    }
}
