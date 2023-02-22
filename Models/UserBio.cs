using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Models
{
   public class UserBio
   {
      [Key]
      public int UserBioId { get; set; }

      public int UserId { get; set; }

      public User User { get; set; }

      [MaxLength(150)]
      public string Firstname { get; set; }

      [MaxLength(150)]
      public string Lastname { get; set; }

      public int DivisionId { get; set; }

      public Division Division { get; set; }
   }
}