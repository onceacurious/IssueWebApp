using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IssueWebApp.Controllers
{
   [Route("api/")]
   [ApiController, Authorize]
   public class UserController : ControllerBase
   {
      private readonly IUserRepository _userRepository;

      public UserController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      [HttpPost("user/register"), AllowAnonymous]
      public async Task<ActionResult<UserAuthDto>> Register([FromBody] UserAuthDto dto)
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

      [HttpPost("user/login"), AllowAnonymous]
      public async Task<ActionResult<UserLoginDto>> Login(UserLoginDto login)
      {
         try
         {
            var result = await _userRepository.Login(login);
            if (result == null)
            {
               return NotFound("User not found");
            }
            var refreshToken = _userRepository.GenerateRefreshtoken();
            SetRefreshToken(refreshToken);
            return Ok(result);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
         }
      }

      [HttpPut("user/{username}")]
      public async Task<ActionResult<UserDto>> UpdateUser(UserDto dto, string username)
      {
         //var result = await _userRepository.UpdateUser(dto, username);
         //if (result == null)
         //{
         //   return NotFound("User not found");
         //}

         //return Ok(result.AsUserDto());
         return null;
      }

      [HttpGet("users")]
      //[AllowAnonymous]
      [Authorize(Roles = "administrator")]
      public async Task<ActionResult<UserDto>> GetUsers()
      {
         var results = (await _userRepository.GetUsers()).Select(u => u.AsUserAuthDto());
         return Ok(results);
      }

      [HttpGet("user/{username}")]
      public async Task<ActionResult<UserDto>> GetUser(string username)
      {
         //string name = User.Identity.Name;
         string nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
         if (username != nameClaim)
         {
            return StatusCode(StatusCodes.Status403Forbidden);
         }
         var result = await _userRepository.GetUser(username);
         return Ok(result);
      }

      [NonAction]
      private void SetRefreshToken(RefreshToken refresh)
      {
         User user = new();

         var cookieOptions = new CookieOptions
         {
            HttpOnly = true,
            Expires = refresh.Expires
         };

         Response.Cookies.Append("refreshToken", refresh.Token, cookieOptions);
      }

      [NonAction]
      private RefreshToken GenerateRefreshtoken()
      {
         byte[] randomBytes = new byte[64];
         using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
         {
            rng.GetBytes(randomBytes);
         }
         var token = Convert.ToBase64String(randomBytes);
         var refreshToken = new RefreshToken
         {
            Token = token,
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
         };

         return refreshToken;
      }
   }
}