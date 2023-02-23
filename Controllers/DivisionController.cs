using IssueWebApp.Dtos.Division;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   //[Authorize]
   [Route("api/")]
   [ApiController]
   public class DivisionController : ControllerBase
   {
      private readonly IDivisionRepository _divisionRepository;
      private readonly ILogger<DivisionController> _logger;

      public DivisionController(IDivisionRepository divisionRepository, ILogger<DivisionController> logger)
      {
         _divisionRepository = divisionRepository;
         _logger = logger;
      }

      //Get: api/divisions
      [HttpGet("divisions")]
      public async Task<ActionResult<DivisionDto>> GetDivisions()
      {
         var objects = (await _divisionRepository.GetDivisions()).Select(d => d.AsDivisionDto());
         return Ok(objects);
      }

      //Get: api/division/id
      [HttpGet("division/{id:int}")]
      public async Task<ActionResult<DivisionDto>> GetDivision(int id)
      {
         var result = await _divisionRepository.GetDivision(id);
         if (result is null)
         {
            return NotFound("Division not found");
         }
         return Ok(result);
      }

      //Post: api/division/
      [HttpPost("division")]
      public async Task<ActionResult<AddDivisionDto>> CreateDivision([FromBody] AddDivisionDto dto)
      {
         try
         {
            Division division = new()
            {
               Name = dto.Name,
            };
            await _divisionRepository.CreateDivision(division);
            return CreatedAtAction(nameof(GetDivision), new { divisionId = division.DivisionId }, division.AsDivisionDto());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }

      //Put: api.division/id/
      [HttpPut("division/{id:int}")]
      public async Task<ActionResult<UpdateDivisionDto>> UpdateDivision(int id, UpdateDivisionDto dto)
      {
         try
         {
            var result = await _divisionRepository.UpdateDivision(id, dto);
            if (result is null)
            {
               return NotFound("Division not found");
            }
            return Ok(result.AsDivisionDto());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }

      //Delete: api/division/id/
      [HttpDelete("division/{id:int}")]
      public async Task<ActionResult<DivisionDto>> DeleteDivision(int id)
      {
         var result = await _divisionRepository.DeleteDivision(id);
         if (result is null)
         {
            return NotFound("Division not fount");
         }
         return Ok(result.AsDivisionDto());
      }
   }
}