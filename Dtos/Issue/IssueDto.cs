using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Issue
{
   public class IssueDto
   {
      [Key]
      public int IssueId { get; set; }

      [Required]
      public string Subject { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }
      [Required]
      [MinLength(5)]
      public string RawText { get; set; }

      [Required]
      [RegularExpression("yes|no")]
      public string OverdueFlag { get; set; }

      [Required]
      [RegularExpression("open|close")]
      public string Status { get; set; }

      public DateTime RaisedDate { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;
      public DateTime DateClosed { get; set; }

      public int DivisionId { get; set; }

      public object IssueAnswers { get; set; }
      public object IssueComments { get; set; }
   }
}