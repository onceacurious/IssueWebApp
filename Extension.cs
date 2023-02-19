using IssueWebApp.Dtos.Division;
using IssueWebApp.Dtos.Issue;
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
            Id = obj.Id,
            OverdueFlag = obj.OverdueFlag,
            RaisedDate = obj.RaisedDate,
            Status = obj.Status,
            Title = obj.Title,
            DivisionId = obj.DivisionId,
         };
      }
   }
}