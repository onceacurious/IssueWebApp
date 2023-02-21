using IssueWebApp.Data;
using IssueWebApp.Dtos.Issue;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories
{
   public class IssueRepository : IIssueRepository
   {
      private readonly ApplicationDbContext _context;
      private readonly IConfiguration _configuration;

      public IssueRepository(ApplicationDbContext context, IConfiguration configuration)
      {
         _context = context;
         _configuration = configuration;
      }

      public async Task<Issue> CreateIssue(Issue issue)
      {
         var result = await _context.Issues.AddAsync(issue);
         await _context.SaveChangesAsync();
         return result.Entity;
      }

      public async Task<Issue> DeleteIssue(int id)
      {
         var result = await _context.Issues.FindAsync(id);
         if (result is not null)
         {
            _context.Issues.Remove(result);
            await _context.SaveChangesAsync();
            return result;
         }
         return null;
      }

      public async Task<Division> GetDivision(int divisionId)
      {
         var division = await _context.Divisions
            .SingleOrDefaultAsync(d => d.DivisionId == divisionId);
         return division;
      }

      public Task<Issue> GetIssue(int id)
      {
         var result = _context.Issues
            .Include(a => a.Answers)
            .Include(c => c.Comments)
            .SingleOrDefaultAsync(i => i.IssueId == id);
         return result;
      }

      public async Task<IEnumerable<Issue>> GetIssueByDivision(int divisionId)
      {
         var results = await _context.Issues
            .Where(d => d.DivisionId == divisionId).ToListAsync();
         return results;
      }

      public async Task<IEnumerable<Issue>> GetIssueByFlag(string flag)
      {
         var result = await _context.Issues
            .Where(i => i.OverdueFlag == flag).ToListAsync();
         return result;
      }

      public async Task<IEnumerable<Issue>> GetIssueByStatus(string status)
      {
         var results = await _context.Issues
            .Where(i => i.Status == status).ToListAsync();
         return results;
      }

      public async Task<IEnumerable<Issue>> GetIssues()
      {
         var result = await _context.Issues.ToListAsync();
         return result;
      }

      public async Task<IEnumerable<Issue>> GetIssueAnswers(int issueId)
      {
         var results = await _context.Issues
            .Include(a => a.Answers)
            .Where(i => i.IssueId == issueId)
            .ToListAsync();
         return results;
      }

      public async Task<Issue> UpdateIssue(int id, UpdateIssueDto dto)
      {
         var result = await _context.Issues.FindAsync(id);
         if (result is not null)
         {
            result.Title = dto.Title;
            result.Status = dto.Status;
            result.OverdueFlag = dto.OverdueFlag;
            result.DateUpdated = DateTimeOffset.Now;
            result.Division = await _context.Divisions
               .SingleOrDefaultAsync(d => d.DivisionId == dto.DivisionId);
            result.DateClosed = dto.Status == "closed" ? DateTime.Now : DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
            return result;
         }

         return null;
      }
   }
}