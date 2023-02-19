using IssueWebApp.Dtos.User;
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
   public class UserController : ControllerBase
   {
      private readonly IUserRepository _userRepository;

      public UserController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      [HttpPost("register")]
      public async Task<ActionResult<UserLoginDto>> Register([FromBody] UserLoginDto dto)
      {
         try
         {
            var result = await _userRepository.Register(dto);
            if (result == null)
            {
               return NotFound("Division not found");
            }
            return Ok(result);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }

      [HttpPost("login")]
      public async Task<ActionResult<UserLoginDto>> Login(UserLoginDto login)
      {
         try
         {
            var result = await _userRepository.Login(login);
            if (result == null)
            {
               return NotFound("User not found");
            }
            return Ok(result);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }
   }
}