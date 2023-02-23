using IssueWebApp.Dtos.Comment;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   [Route("api/")]
   [ApiController]
   public class CommentController : ControllerBase
   {
      private readonly ICommentRepository _commentRepository;
      private readonly IAnswerRepository _answerRepository;
      private readonly IIssueRepository _issueRepository;

      public CommentController(IIssueRepository issueRepository, ICommentRepository commentRepository, IAnswerRepository answerRepository)
      {
         _commentRepository = commentRepository;
         _answerRepository = answerRepository;
         _issueRepository = issueRepository;
      }

      [HttpPost("comment")]
      public async Task<ActionResult<PostCommentDto>> PostComment([FromBody] PostCommentDto dto)
      {
         var answer = await _answerRepository.GetAnswer(dto.AnswerId.GetValueOrDefault());
         var issue = await _issueRepository.GetIssue(dto.IssueId.GetValueOrDefault());

         Comment comment = new()
         {
            Description = dto.Description,
            DateCreated = DateTime.Now,
            Answer = answer,
            Issue = issue,
            UserId = dto.UserId,
            //AnswerId = (int)dto.AnswerId.Value,
            //IssueId = (int)dto.IssueId.Value
         };

         await _commentRepository.PostComment(comment);
         return CreatedAtAction(nameof(GetComment), new { commentId = comment.CommentId }, comment.AsCommentDto());
      }

      [HttpGet("comment/{commentId:int}")]
      public async Task<ActionResult<Comment>> GetComment(int commentId)
      {
         var result = await _commentRepository.GetComment(commentId);
         return Ok(result);
      }
   }
}