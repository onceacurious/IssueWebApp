using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Models
{
   public class User
   {
      [Key]
      public int UserId { get; set; }

      [Required]
      [MaxLength(150)]
      [MinLength(4)]
      public string Username { get; set; }

      public byte[] PasswordHash { get; set; }
      public byte[] PasswordSalt { get; set; }

      [MaxLength(150)]
      public string Firstname { get; set; }

      [MaxLength(150)]
      public string Lastname { get; set; }

      public string Role { get; set; }

      public int DivisionId { get; set; }

      public Division Division { get; set; }
   }
}