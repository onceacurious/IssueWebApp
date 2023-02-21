using System.ComponentModel.DataAnnotations;
using System;

namespace IssueWebApp.Dtos.Comment
{
   public class CommentDto
   {
      [Key]
      public int CommentId { get; set; }

      [Required]
      [MaxLength(500), MinLength(5)]
      public string Description { get; set; }

      public bool IsAccepted { get; set; } = false;
      public bool IsDeleted { get; set; } = false;

      public DateTime DateCreated { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;
      public int UserId { get; set; }

      public int? AnswerId { get; set; }

      public int? IssueId { get; set; }

   }
}