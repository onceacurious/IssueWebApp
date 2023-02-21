using System.ComponentModel.DataAnnotations;
using System;

namespace IssueWebApp.Dtos.Answer
{
   public class UpdateAnswerDto
   {
      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

      [Required]
      public int IssueId { get; set; }

      [Required]
      public int UserId { get; set; }

      public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;

      [Required]
      public bool IsSolution { get; set; }

      [Required]
      public bool IsDeleted { get; set; }
   }
}