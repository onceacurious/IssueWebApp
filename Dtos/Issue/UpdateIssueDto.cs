using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Issue
{
   public class UpdateIssueDto
   {
      [Required]
      public string Title { get; set; }

      public DateTimeOffset DateUpdated { get; set; }

      [Required]
      [RegularExpression("yes|no")]
      public string OverdueFlag { get; set; }

      [Required]
      [RegularExpression("open|closed")]
      public string Status { get; set; }

      public int DivisionId { get; set; }
   }
}