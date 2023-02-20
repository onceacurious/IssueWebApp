using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IUserRepository
   {
      Task<IEnumerable<User>> GetUsers();

      Task<string> Login(UserLoginDto login);

      Task<User> Register(UserAuthDto dto);

      Task<User> UpdateUser(UserBio user, string username);

      Task<User> GetUser(int userId);

      Task<User> GetUser(string username);

      RefreshToken GenerateRefreshtoken();
   }
}