using IssueWebApp.Data;
using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories
{
   public class UserRepository : IUserRepository
   {
      private readonly ApplicationDbContext _context;
      private readonly IConfiguration _configuration;

      public UserRepository(ApplicationDbContext context, IConfiguration configuration)
      {
         _context = context;
         _configuration = configuration;
      }

      public async Task<User> Register(UserRegisterDto dto)
      {
         var username = dto.Username.Trim();
         var division = await _context.Divisions.SingleOrDefaultAsync(d => d.DivisionId == dto.DivisionId);
         var exist = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
         if (exist is not null && (username.Length <= 3 || username.ToLower() == "username" || exist.Username == username))
         {
            throw new ArgumentException("Invalid Username");
         }
         CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
         User user = new();
         user.Username = username;
         user.PasswordHash = passwordHash;
         user.PasswordSalt = passwordSalt;
         user.Division = division is null ? throw new AggregateException("division not found") : division;

         var result = await _context.Users.AddAsync(user);
         await _context.SaveChangesAsync();
         return result.Entity;
      }

      public async Task<UserTokenDto> Login(UserLoginDto login)
      {
         var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == login.Username);
         if (user != null)
         {
            if (!Authenticated(login.Password, user.PasswordHash, user.PasswordSalt))
            {
               throw new ArgumentException("Incorrect password");
            }
            string token = CreateToken(user);
            UserTokenDto userToken = new();
            userToken.Token = token;
            return userToken;
         }
         return null;
      }

      public async Task<User> UpdateUser(UserDto user, string username)
      {
         var _user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
         if (_user != null)
         {
            var division = await _context.Divisions.SingleOrDefaultAsync(d => d.DivisionId == _user.DivisionId);
            if (division is not null)
            {
               _user.Division = division;
               _user.Firstname = user.Firstname;
               _user.Lastname = user.Lastname;
               _user.Role = user.Role;
               await _context.SaveChangesAsync();
               return _user;
            }
            throw new ArgumentException("Division not found");
         }
         return null;
      }

      public async Task<IEnumerable<User>> GetUsers()
      {
         var results = await _context.Users.ToListAsync();
         return results;
      }

      private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
      {
         using (var hmac = new HMACSHA512())
         {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
         }
      }

      private bool Authenticated(string password, byte[] passwordHash, byte[] passwordSalt)
      {
         using (var hmac = new HMACSHA512(passwordSalt))
         {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
         }
      }

      private string CreateToken(User user)
      {
         List<Claim> claims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
         };

         var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
         var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
         var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
            );
         var jwt = new JwtSecurityTokenHandler().WriteToken(token);

         return jwt;
      }
   }
}