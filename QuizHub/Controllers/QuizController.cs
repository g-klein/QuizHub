﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using QuizHub.Database.Interfaces;
using QuizHub.Extensions;
using QuizHub.Models.Requests;
using QuizHub.Services;

namespace QuizHub.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("{Name}")]
        [EnableCors("TestPolicy")]
        public async Task<IActionResult> CreateQuiz(string Name)
        {
            var userId = HttpContext.GetUserIdFromJwt();
            var quiz = await _quizService.CreateQuiz(Name, userId);
            return Ok(new { quiz });
        }

        [HttpDelete("{Id}")]
        [EnableCors("TestPolicy")]
        public async Task<IActionResult> DeleteQuiz(string Id)
        {
            var userId = HttpContext.GetUserIdFromJwt();
            try
            {
                var quizId = new ObjectId(Id);
                var quiz = await _quizService.GetQuiz(quizId);
                if (quiz == null)
                {
                    return StatusCode(StatusCodes.Status410Gone, new { message = "Error: quiz does not exist" });
                }

                if (quiz.OwnerId == userId)
                {
                    _quizService.DeleteQuiz(quizId);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{QuizId}/Questions")]
        public async Task<IActionResult> AddQuestion([FromBody] AddQuestionRequest request, string QuizId)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            var userId = HttpContext.GetUserIdFromJwt();
            try
            {
                var quizId = new ObjectId(QuizId);
                var quiz = await _quizService.GetQuiz(quizId);
                if (quiz == null)
                {
                    return StatusCode(StatusCodes.Status410Gone, new { message = "Error: quiz does not exist" });
                }

                if (quiz.OwnerId == userId)
                {
                    var result = await _quizService.AddQuestion(quizId, request.Question, request.Answer);
                    return Ok(new { quiz = result });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{QuizId}/Questions/{Id}")]
        public async Task<IActionResult> DeleteQuestion(string QuizId, string Id)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            var userId = HttpContext.GetUserIdFromJwt();
            try
            {
                var quizId = new ObjectId(QuizId);
                var quiz = await _quizService.GetQuiz(quizId);
                if (quiz == null)
                {
                    return StatusCode(StatusCodes.Status410Gone, new { message = "Error: quiz does not exist" });
                }

                if (quiz.OwnerId == userId)
                {
                    quiz = await _quizService.DeleteQuizAsync(QuizId, Id);
                    return Ok(quiz);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}