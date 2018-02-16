using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using QuizHub.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizHub.Controllers
{
    [Route("api/[controller]")]
    public class QuizListController : Controller
    {
        [HttpGet]
        [EnableCors("TestPolicy")]
        public async Task<IActionResult> Index(string UserId)
        {
            var jwtUser = HttpContext.GetUserIdFromJwt();
            if(jwtUser != new ObjectId(UserId))
                return new UnauthorizedResult();

            //get list of quizzes for user
            //return them

            throw new NotImplementedException();
        }
    }
}
