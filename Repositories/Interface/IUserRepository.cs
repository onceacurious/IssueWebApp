using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IUserRepository
   {
      Task<User> Register(UserRegisterDto dto);

      Task<string> Login(UserLoginDto login);

      Task<User> UpdateUser(UserDto user, string username);

      Task<IEnumerable<User>> GetUsers();
   }
}