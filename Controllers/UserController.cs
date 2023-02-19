using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   [Route("api/")]
   [ApiController]
   [Authorize]
   public class UserController : ControllerBase
   {
      private readonly IUserRepository _userRepository;

      public UserController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      [AllowAnonymous]
      [HttpPost("user/register")]
      public async Task<ActionResult<UserRegisterDto>> Register([FromBody] UserRegisterDto dto)
      {
         try
         {
            var result = await _userRepository.Register(dto);
            if (result == null)
            {
               return NotFound("Division not found");
            }
            return Ok(result.AsUserDto());
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }

      [AllowAnonymous]
      [HttpPost("user/login")]
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

      [Authorize()]
      [HttpPut("user/{username}")]
      public async Task<ActionResult<UserDto>> UpdatUser(UserDto dto, string username)
      {
         var result = await _userRepository.UpdateUser(dto, username);
         if (result == null)
         {
            return NotFound("User not found");
         }

         return Ok(result.AsUserDto());
      }

      [Authorize(Roles = "Administrator")]
      [HttpGet("users")]
      public async Task<ActionResult<UserDto>> GetUsers()
      {
         var results = (await _userRepository.GetUsers()).Select(u => u.AsUserDto());
         return Ok(results);
      }
   }
}