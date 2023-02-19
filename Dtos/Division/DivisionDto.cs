using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Division
{
   public class DivisionDto
   {
      [Key]
      public int DivisionId { get; set; }

      [Required]
      public string Name { get; set; }
   }
}