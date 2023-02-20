using System;
using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Models
{
   public class RefreshToken
   {
      [Key]
      public Guid RefreshTokenId { get; set; } = Guid.NewGuid();

      public string Token { get; set; }
      public DateTime Created { get; set; } = DateTime.Now;
      public DateTime Expires { get; set; } = DateTime.Now.AddDays(7);
   }
}