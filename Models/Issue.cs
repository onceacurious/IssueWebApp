using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Models
{
   public enum Status
   {
      Open,
      Closed,
   }

   public enum Flag
   {
      Yes,
      No,
   }

   public class Issue
   {
      [Key]
      public int IssueId { get; set; }

      [Required]
      [MaxLength(150), MinLength(5)]
      public string Subject { get; set; }

      [Required]
      [MinLength(5)]
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

      [Required]
      public DateTime RaisedDate { get; set; } = DateTime.Now;

      public DateTime DateClosed { get; set; }
      public DateTimeOffset DateUpdated { get; set; }

      public bool IsAttended { get; set; } = false;

      public ICollection<Comment> Comments { get; set; }

      public ICollection<Answer> Answers { get; set; }

      public int DivisionId { get; set; }

      [JsonIgnore]
      public Division Division { get; set; }
   }
}