using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IUserRepository
   {
      Task<User> Register(UserLoginDto dto);

      Task<UserTokenDto> Login(UserLoginDto login);
   }
}