using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IssueWebApp.Models
{
   public class Comment
   {
      [Key]
      public int CommentId { get; set; }

      [Required]
      [MaxLength(500), MinLength(5)]
      public string Description { get; set; }

      public bool IsAccepted { get; set; } = false;

      public DateTime DateCreated { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;
      public int UserId { get; set; }

      [JsonIgnore]
      public User Author { get; set; }

      public int AnswerId { get; set; }

      [JsonIgnore]
      public Answer Answer { get; set; }

      public int IssueId { get; set; }

      [JsonIgnore]
      public Issue Issue { get; set; }
   }
}