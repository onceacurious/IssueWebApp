using IssueWebApp.Dtos.Issue;
using IssueWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IIssueRepository
   {
      Task<IEnumerable<Issue>> GetIssues();

      Task<Issue> GetIssue(int id);

      Task<Issue> CreateIssue(Issue issue);

      Task<Issue> UpdateIssue(int id, UpdateIssueDto dto);

      Task<Issue> DeleteIssue(int id);

      Task<IEnumerable<Issue>> GetIssueByDivision(int divisionId);

      Task<Division> GetDivision(int divisionId);

      Task<IEnumerable<Issue>> GetIssueByStatus(int status);

      Task<IEnumerable<Issue>> GetIssueByFlag(int flag);
   }
}