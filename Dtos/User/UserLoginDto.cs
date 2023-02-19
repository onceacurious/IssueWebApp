using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.User
{
   public class UserLoginDto
   {
      [Required]
      [MaxLength(150)]
      public string Username { get; set; }

      [Required]
      [MaxLength(150)]
      public string Password { get; set; }
   }
}