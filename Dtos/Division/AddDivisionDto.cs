using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Division
{
   public class AddDivisionDto
   {
      [Required]
      public string Name { get; set; }
   }
}