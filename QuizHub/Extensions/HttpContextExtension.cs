using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using QuizHub.Models.Auth;
using QuizHub.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHub.Extensions
{
    public static class HttpContextExtension
    {
        public static ObjectId GetUserIdFromJwt(this HttpContext context)
        {
            try
            {
                var headers = context.Request.Headers;
                var auth = headers["Authorization"].ToString();
                var jwt = auth.Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                var UserId = token.Claims.FirstOrDefault(x => x.Type == "UserId");

                if (UserId != null)
                    return new ObjectId(UserId.Value);

                throw new JwtReadingException();
            }
            catch
            {
                throw new JwtReadingException();
            }
        }
    }
}
