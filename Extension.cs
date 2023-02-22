using IssueWebApp.Dtos.Answer;
using IssueWebApp.Dtos.Comment;
using IssueWebApp.Dtos.Division;
using IssueWebApp.Dtos.Issue;
using IssueWebApp.Dtos.User;
using IssueWebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            Subject = obj.Subject,
            Description = obj.Description,
            RawText = obj.RawText,
            OverdueFlag = obj.OverdueFlag,
            Status = obj.Status,
            DivisionId = obj.DivisionId,
            RaisedDate = obj.RaisedDate,
            DateUpdated = obj.DateUpdated,
            DateClosed = obj.DateClosed,
            IssueAnswers = obj.Answers,
            IssueComments = obj.Comments,
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
            RawText = obj.RawText,
            UserId = obj.UserId,
            IssueId = obj.IssueId,
            DateCreated = obj.DateCreated,
            DateUpdated = obj.DateUpdated,
            IsDeleted = obj.IsDeleted,
            IsSolution = obj.IsSolution,
            AuthorName = obj.Author.Username,
            Comments = obj.Comments,
         };
      }

      public static CommentDto AsCommentDto(this Comment obj)
      {
         return new CommentDto
         {
            CommentId = obj.CommentId,
            Description = obj.Description,
            IsAccepted = obj.IsAccepted,
            IsDeleted = obj.IsDeleted,
            DateCreated = obj.DateCreated,
            DateUpdated = obj.DateUpdated,
            UserId = obj.UserId,
            AnswerId = obj.AnswerId,
            IssueId = obj.IssueId,
         };
      }
   }
}