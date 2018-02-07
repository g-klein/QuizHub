using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizHub.Models.ConfigurationModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizHub.Auth
{
    public class QuizJwt : IGenerateJwts, IValidateJwts
    {
        private TokenSettings _tokenSettings;

        public QuizJwt(IOptions<TokenSettings> tokenOptions)
        {
            _tokenSettings = tokenOptions.Value;
        }

        public string GetJwtString(string customerId, string email)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow().AddMinutes(60 * 24);
            var unixEpoch = JwtValidator.UnixEpoch;
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                { "UserId", customerId },
                { "UserEmail", email },
                { "exp", secondsSinceEpoch }
            };

            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, _tokenSettings.Key);
        }

        public void ValidateJwt(string Jwt)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            decoder.Decode(Jwt, _tokenSettings.Key, verify: true);
        }
    }
}
