using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IssueWebApp.Models
{
   public class Division
   {
      [Key]
      public int DivisionId { get; set; }

      [Required]
      public string Name { get; set; }

      [JsonIgnore]
      public ICollection<Issue> Issue { get; set; }

      [JsonIgnore]
      public ICollection<User> Users { get; set; }
   }
}