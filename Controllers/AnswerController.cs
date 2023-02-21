using IssueWebApp.Dtos.Answer;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   [Route("api/")]
   [ApiController]
   public class AnswerController : ControllerBase
   {
      private readonly IAnswerRepository _answerRepository;
      private readonly IIssueRepository _issueRepository;
      private readonly IUserRepository _userRepository;

      public AnswerController(IAnswerRepository answerRepository, IUserRepository userRepository, IIssueRepository issueRepository)
      {
         _answerRepository = answerRepository;
         _userRepository = userRepository;
         _issueRepository = issueRepository;
      }

      [HttpGet("answer/{answerId:int}")]
      public async Task<ActionResult<AnswerDto>> GetAnswer(int answerId)
      {
         var result = await _answerRepository.GetAnswer(answerId);
         return Ok(result.AsAnswerDto());
      }

      [HttpPost("answer")]
      public async Task<ActionResult<PostAnswerDto>> PostAnswer([FromBody] PostAnswerDto dto)
      {
         var issue = await _issueRepository.GetIssue(dto.IssueId);
         var user = await _userRepository.GetUser(dto.UserId);
         Answer answer = new()
         {
            Description = dto.Description,
            //Issue = issue,
            Author = user,
            DateCreated = DateTime.Now,
         };

         await _answerRepository.PostAnswer(answer);
         return CreatedAtAction(nameof(GetAnswer), new { answerId = answer.AnswerId }, answer.AsAnswerDto());
      }

      [HttpPut("answer/{answerId:int}")]
      [Authorize]
      public async Task<ActionResult<UpdateAnswerDto>> UpdateAnswer(int answerId, UpdateAnswerDto dto)
      {
         try
         {
            var result = await _answerRepository.UpdatedAnswer(answerId, dto);
            if (result is null)
            {
               return NotFound("Answer not found");
            }
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var owner = await _userRepository.GetUser(result.UserId);
            if (name != owner.Username)
            {
               return StatusCode(StatusCodes.Status403Forbidden);
            }
            return Ok(result.AsAnswerDto());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }
   }
}