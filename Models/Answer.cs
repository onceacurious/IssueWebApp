using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueWebApp.Models
{
   public class Answer
   {
      [Key]
      public int AnswerId { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

      public bool IsSolution { get; set; } = false;
      public bool IsDeleted { get; set; } = false;

      [ForeignKey("Issue")]
      public int IssueId { get; set; }

      [JsonIgnore]
      public Issue Issue { get; set; }

      [ForeignKey("User")]
      public int UserId { get; set; }

      [JsonIgnore]
      public virtual User Author { get; set; }

      public DateTime DateCreated { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; }
      public ICollection<Comment> Comments { get; set; }
   }
}