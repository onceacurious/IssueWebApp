using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
      OnProgress,
   }

   public class Issue
   {
      [Key]
      public int Id { get; set; }

      [Required]
      public string Title { get; set; }

      [Required]
      [Column(TypeName = "text")]
      public Flag OverdueFlag { get; set; }

      [Required]
      [Column(TypeName = "text")]
      public Status Status { get; set; }

      [Required]
      public DateTimeOffset RaisedDate { get; set; } = DateTimeOffset.UtcNow;

      public int DivisionId { get; set; }

      //public string DivisionName { get; set; }
      public Division Division { get; set; }
   }
}