using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.User
{
   public class UserDto
   {
      [Required]
      [MaxLength(150)]
      [MinLength(4)]
      public string Username { get; set; }

      [Required]
      [MaxLength(150)]
      public string Firstname { get; set; }

      [Required]
      [MaxLength(150)]
      public string Lastname { get; set; }

      [Required]
      public string Role { get; set; }

      [Required]
      public int DivisionId { get; set; }
   }
}