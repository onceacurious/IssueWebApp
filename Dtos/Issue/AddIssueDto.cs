using System.ComponentModel.DataAnnotations;

namespace IssueWebApp.Dtos.Issue
{
   public class AddIssueDto
   {
      [Required]
      public string Title { get; set; }

      [Required]
      [MaxLength(1000), MinLength(5)]
      public string Description { get; set; }

      [Required]
      [RegularExpression("yes|no")]
      public string OverdueFlag { get; set; }

      [Required]
      [RegularExpression("open|close")]
      public string Status { get; set; }

      [Required]
      public int DivisionId { get; set; }
   }
}