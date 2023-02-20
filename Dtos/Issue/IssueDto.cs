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
      public string Title { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

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
   }
}