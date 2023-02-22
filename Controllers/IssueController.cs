using IssueWebApp.Dtos.Issue;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   [Route("api/")]
   [ApiController]
   public class IssueController : ControllerBase
   {
      private readonly IIssueRepository _issueRepository;
      private readonly ILogger<IssueController> _logger;
      private readonly IAnswerRepository _answerRepository;

      public IssueController(IIssueRepository repository, ILogger<IssueController> logger, IAnswerRepository answerRepository)
      {
         _logger = logger;
         _issueRepository = repository;
         _answerRepository = answerRepository;
      }

      [HttpGet("issues")]
      public async Task<ActionResult<Issue>> GetIssues()
      {
         var results = (await _issueRepository.GetIssues())
            .Select(i => i.AsIssueDto());
         return Ok(results);
      }

      [HttpGet("issue/{issueId:int}")]
      public async Task<ActionResult<Issue>> GetIssue(int issueId)
      {
         var result = await _issueRepository.GetIssue(issueId);
         if (result is null)
         {
            return NotFound("Issue not found");
         }

         return Ok(result.AsIssueDto());
      }

      [HttpGet("issue/{issueId:int}/answer")]
      public async Task<ActionResult<Issue>> GetIssueComments(int issueId)
      {
         var results = await _issueRepository.GetIssueAnswers(issueId);
         return Ok(results);
      }

      [HttpGet("issues/division/{divisionId:int}")]
      public async Task<ActionResult<IssueDto>> GetIssueByDivision(int divisionId)
      {
         var results = (await _issueRepository.GetIssueByDivision(divisionId)).Select(i => i.AsIssueDto());
         return Ok(results);
      }

      //Open Issues
      [HttpGet("issues/open")]
      public async Task<ActionResult<IssueDto>> GetOpenIssues()
      {
         var result = await _issueRepository.GetIssueByStatus("open");
         return Ok(result);
      }

      //Close Issues
      [HttpGet("issues/closed")]
      public async Task<ActionResult<IssueDto>> GetClosedIssued()
      {
         var result = await _issueRepository.GetIssueByStatus("closed");
         return Ok(result);
      }

      //Overdue Issues
      [HttpGet("issues/overdue")]
      public async Task<ActionResult<IssueDto>> GetOverdueIssues()
      {
         var result = await _issueRepository.GetIssueByFlag("open");
         return Ok(result);
      }

      [HttpPost("issue")]
      public async Task<ActionResult<AddIssueDto>> CreateIssue([FromBody] AddIssueDto dto)
      {
         var division = await _issueRepository.GetDivision(dto.DivisionId);
         Issue issue = new()
         {
            Subject = dto.Subject,
            Description = dto.Description,
            RawText = dto.RawText,
            OverdueFlag = dto.OverdueFlag,
            Status = dto.Status,
            RaisedDate = DateTime.Now,
            Division = division
         };
         await _issueRepository.CreateIssue(issue);
         return CreatedAtAction(nameof(GetIssue), new { issueId = issue.IssueId }, issue.AsIssueDto());
      }

      [HttpDelete("issue/{id:int}")]
      public async Task<ActionResult<IssueDto>> DeleteIssue(int id)
      {
         var result = await _issueRepository.DeleteIssue(id);
         if (result is null)
         {
            return NotFound("Issue not found");
         }
         return Ok(result.AsIssueDto());
      }

      [HttpPut("issue/{id:int}")]
      public async Task<ActionResult<UpdateIssueDto>> UpdateIssue(int id, UpdateIssueDto dto)
      {
         var result = await _issueRepository.UpdateIssue(id, dto);
         if (result is null)
         {
            return NotFound("Issue not found");
         }

         return Ok(result.AsIssueDto());
      }
   }
}