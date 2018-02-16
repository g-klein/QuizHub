using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizHub.Auth;
using QuizHub.Models.ConfigurationModels;
using QuizHub.Models.Exceptions;
using QuizHub.Models.Requests;
using QuizHub.Services;

namespace MyCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        IGenerateJwts _jwtGenerator;
        IRegisterUserService _registerUserService;
        ILoginService _loginService;

        public UsersController(IGenerateJwts jwtGenerator, IRegisterUserService registerUserService, ILoginService loginService)
        {
            _jwtGenerator = jwtGenerator;
            _registerUserService = registerUserService;
            _loginService = loginService;
        }

        [HttpPost("Login")]
        [EnableCors("TestPolicy")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            try
            {
                var jwt = await _loginService.Login(user.Email, user.Password);
                return Ok(new { authToken = jwt });
            }
            //TODO: catch unauthorized exception
            catch (LoginFailedException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }


        [HttpPost("Register")]
        [EnableCors("TestPolicy")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            try
            {
                var userId = await _registerUserService.RegisterUser(user.Email, user.Password);
                var jwt = _jwtGenerator.GetJwtString(userId.ToString(), user.Email);
                return Ok(new { authToken = jwt });
            }
            catch (DuplicateUserException)
            {
                return new BadRequestObjectResult(new { message = "User " + user.Email + " already exists."});
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}