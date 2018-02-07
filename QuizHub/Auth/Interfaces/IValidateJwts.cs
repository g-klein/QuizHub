using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Auth
{
    interface IValidateJwts
    {
        void ValidateJwt(string Jwt);
    }
}
