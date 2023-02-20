﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

      public int IssueId { get; set; }

      [JsonIgnore]
      public Issue Issue { get; set; }

      public int UserId { get; set; }

      [JsonIgnore]
      public User Author { get; set; }

      public DateTime DateCreated { get; set; } = DateTime.Now;
      public DateTimeOffset DateUpdated { get; set; }
      public ICollection<Comment> Comments { get; set; }
   }
}