using Newtonsoft.Json;
using System;
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

      [RegularExpression("administrator|user|staff|officer")]
      public string Role { get; set; } = "user";

      public Guid RefreshTokenId { get; set; }
      public RefreshToken RefreshToken { get; set; }
   }
}