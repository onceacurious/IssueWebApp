using IssueWebApp.Dtos.Comment;
using IssueWebApp.Models;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface ICommentRepository
   {
      Task<Comment> PostComment(Comment comment);

      Task<Comment> GetComment(int commentId);
   }
}