using IssueWebApp.Dtos.Answer;
using IssueWebApp.Dtos.Division;
using IssueWebApp.Dtos.Issue;
using IssueWebApp.Dtos.User;
using IssueWebApp.Models;

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
            IssueId = obj.IssueId,
            Description = obj.Description,
            OverdueFlag = obj.OverdueFlag,
            Status = obj.Status,
            Title = obj.Title,
            DivisionId = obj.DivisionId,
            RaisedDate = obj.RaisedDate,
            DateUpdated = obj.DateUpdated,
            DateClosed = obj.DateClosed,
         };
      }

      public static UserDto AsUserDto(this UserBio obj)
      {
         return new UserDto
         {
            UserId = obj.UserId,
            Firstname = obj.Firstname,
            Lastname = obj.Lastname,
            DivisionId = obj.DivisionId,
         };
      }

      public static UserDto AsUserAuthDto(this User obj)
      {
         return new UserDto
         {
            UserId = obj.UserId,
            Username = obj.Username,
            Role = obj.Role,
            PasswordHash = obj.PasswordHash,
            PasswordSalt = obj.PasswordSalt,
         };
      }

      public static AnswerDto AsAnswerDto(this Answer obj)
      {
         return new AnswerDto
         {
            AnswerId = obj.AnswerId,
            Description = obj.Description,
            UserId = obj.UserId,
            IssueId = obj.IssueId,
            DateCreated = obj.DateCreated,
            DateUpdated = obj.DateUpdated,
         };
      }
   }
}