using IssueWebApp.Dtos.Division;
using IssueWebApp.Dtos.Issue;
using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace IssueWebApp
{
   public static class Extension
   {
      public static DivisionDto AsDivisionDto(this Division obj)
      {
         return new DivisionDto
         {
            DivisionId = obj.DivisionId,
            Name = obj.Name,
         };
      }

      public static IssueDto AsIssueDto(this Issue obj)
      {
         return new IssueDto
         {
            Id = obj.Id,
            OverdueFlag = obj.OverdueFlag,
            RaisedDate = obj.RaisedDate,
            Status = obj.Status,
            Title = obj.Title,
            DivisionId = obj.DivisionId,
         };
      }

      public static UserDto AsUserDto(this User obj)
      {
         return new UserDto
         {
            UserId = obj.UserId,
            Username = obj.Username,
            Firstname = obj.Firstname,
            Lastname = obj.Lastname,
            Role = obj.Role,
            DivisionId = obj.DivisionId,
            PasswordHash = obj.PasswordHash,
            PasswordSalt = obj.PasswordSalt,
         };
      }
   }
}