using IssueWebApp.Data;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories
{
   public class CommentRepository : ICommentRepository
   {
      private readonly ApplicationDbContext _context;

      public CommentRepository(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<Comment> PostComment(Comment comment)
      {
         var result = await _context.Comments.AddAsync(comment);
         await _context.SaveChangesAsync();
         return result.Entity;
      }

      public async Task<Comment> GetComment(int commentId)
      {
         var result = await _context.Comments
            .Include(a => a.Answer)
            .Include(i => i.Issue)
            .SingleOrDefaultAsync(c => c.CommentId == commentId);
         return result;
      }
   }
}