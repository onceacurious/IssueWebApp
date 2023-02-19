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

      public IssueController(IIssueRepository repository, ILogger<IssueController> logger)
      {
         _logger = logger;
         _issueRepository = repository;
      }

      [HttpGet("issues")]
      public async Task<ActionResult<IssueDto>> GetIssues()
      {
         var results = (await _issueRepository.GetIssues())
            .Select(i => i.AsIssueDto());
         return Ok(results);
      }

      [HttpGet("issue/{id:int}")]
      public async Task<ActionResult<IssueDto>> GetIssue(int id)
      {
         var result = await _issueRepository.GetIssue(id);
         if (result is null)
         {
            return NotFound("Issue not found");
         }
         return Ok(result);
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
         var result = (await _issueRepository.GetIssueByStatus(0)).Select(i => i.AsIssueDto());
         return Ok(result);
      }

      //Close Issues
      [HttpGet("issues/closed")]
      public async Task<ActionResult<IssueDto>> GetClosedIssued()
      {
         var result = (await _issueRepository.GetIssueByStatus(1)).Select(i => i.AsIssueDto());
         return Ok(result);
      }

      //Overdue Issues
      [HttpGet("issues/overdue")]
      public async Task<ActionResult<IssueDto>> GetOverdueIssues()
      {
         var result = (await _issueRepository.GetIssueByFlag(0)).Select(i => i.AsIssueDto());
         return Ok(result);
      }

      [HttpPost("issue")]
      public async Task<ActionResult<AddIssueDto>> CreateIssue([FromBody] AddIssueDto dto)
      {
         var division = await _issueRepository.GetDivision(dto.DivisionId);

         Issue issue = new()
         {
            Title = dto.Title,
            OverdueFlag = dto.OverdueFlag,
            Status = dto.Status,
            RaisedDate = DateTimeOffset.UtcNow,
            Division = division
         };
         await _issueRepository.CreateIssue(issue);
         return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, issue.AsIssueDto());
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