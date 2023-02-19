using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Division
{
   public class UpdateDivisionDto
   {
      [Required]
      public string Name { get; set; }
   }
}