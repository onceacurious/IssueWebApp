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

      public CommentController(ICommentRepository commentRepository)
      {
         _commentRepository = commentRepository;
      }

      [HttpPost("comment")]
      public async Task<ActionResult<PostCommentDto>> PostComment([FromBody] PostCommentDto dto)
      {
         Comment comment = new()
         {
            Description = dto.Description,
            DateCreated = DateTime.Now,
            UserId = dto.UserId,
            AnswerId = dto.AnswerId is null ? null : dto.AnswerId,
            IssueId = dto.IssueId is null ? null : dto.IssueId,
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