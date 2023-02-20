using IssueWebApp.Data;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories
{
   public class AnswerRepository : IAnswerRepository
   {
      private readonly ApplicationDbContext _context;
      private readonly IConfiguration _configuration;

      public AnswerRepository(ApplicationDbContext context, IConfiguration configuration)
      {
         _context = context;
         _configuration = configuration;
      }

      public async Task<Answer> PostAnswer(Answer answer)
      {
         var result = await _context.Answers.AddAsync(answer);
         await _context.SaveChangesAsync();
         return result.Entity;
      }

      public async Task<IEnumerable<Answer>> GetAnswerByIssue(int issueId)
      {
         var results = await _context.Answers.Where(a => a.IssueId == issueId).ToListAsync();
         return results;
      }

      public async Task<IEnumerable<Answer>> GetAnswerByUser(int userId)
      {
         var results = await _context.Answers.Where(a => a.UserId == userId).ToListAsync();
         return results;
      }

      public async Task<Answer> GetAnswer(int answerId)
      {
         var result = await _context.Answers.SingleOrDefaultAsync(a => a.AnswerId == answerId);
         return result;
      }
   }
}