using IssueWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueWebApp.Dtos.Issue
{
   public class IssueDto
   {
      [Key]
      public int Id { get; set; }

      [Required]
      public string Title { get; set; }

      [Required]
      [Column(TypeName = "text")]
      public Flag OverdueFlag { get; set; } = Flag.No;

      [Required]
      [Column(TypeName = "text")]
      public Status Status { get; set; } = Status.Open;

      [Required]
      public DateTimeOffset RaisedDate { get; set; }

      public int DivisionId { get; set; }
   }
}