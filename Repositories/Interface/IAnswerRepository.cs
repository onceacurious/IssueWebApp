using IssueWebApp.Dtos.Answer;
using IssueWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IAnswerRepository
   {
      Task<IEnumerable<Answer>> GetAnswerByIssue(int issueId);

      Task<IEnumerable<Answer>> GetAnswerByUser(int userId);

      Task<Answer> PostAnswer(Answer answer);

      Task<Answer> GetAnswer(int answerId);

      Task<Answer> UpdatedAnswer(int answerId, UpdateAnswerDto dto);
   }
}