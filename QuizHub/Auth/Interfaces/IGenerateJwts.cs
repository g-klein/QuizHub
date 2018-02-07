using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Auth
{
    public interface IGenerateJwts
    {
        string GetJwtString(string customerId, string email);
    }
}
