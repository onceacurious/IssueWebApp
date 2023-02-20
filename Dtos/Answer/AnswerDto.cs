using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Answer
{
   public class AnswerDto
   {
      [Key]
      public int AnswerId { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

      public int IssueId { get; set; }
      public int UserId { get; set; }
      public DateTime DateCreated { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; }
   }
}