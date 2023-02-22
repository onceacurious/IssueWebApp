using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Issue
{
   public class UpdateIssueDto
   {
      [Required]
      public string Subject { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

      [Required]
      [MinLength(5)]
      public string RawText { get; set; }

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