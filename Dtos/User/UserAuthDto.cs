using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.User
{
   public class UserAuthDto
   {
      [Required]
      [MaxLength(150)]
      public string Username { get; set; }

      [Required]
      [MaxLength(150)]
      public string Password { get; set; }

      [RegularExpression("administrator|user|staff|officer")]
      public string Role { get; set; }

      public DateTime TokenCreated { get; set; }
      public DateTime TokenExpires { get; set; }
   }
}