using IssueWebApp.Data;
using IssueWebApp.Dtos.Division;
using IssueWebApp.Models;
using IssueWebApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories
{
   public class DivisionRepository : IDivisionRepository
   {
      private readonly ApplicationDbContext _context;
      private readonly IConfiguration _configuration;

      public DivisionRepository(ApplicationDbContext context, IConfiguration configuration)
      {
         _context = context;
         _configuration = configuration;
      }

      public async Task<Division> CreateDivision(Division division)
      {
         var name = division.Name.ToLower().Trim();
         var exist = await _context.Divisions.SingleOrDefaultAsync(d => d.Name.ToLower() == name);
         if (exist is not null)
         {
            throw new ArgumentException("Name already exist");
         }
         else if (name == "division")
         {
            throw new ArgumentException("Invalid name");
         }
         else
         {
            var result = await _context.Divisions.AddAsync(division);
            await _context.SaveChangesAsync();
            return result.Entity;
         }
      }

      public async Task<Division> DeleteDivision(int id)
      {
         var result = await _context.Divisions.FindAsync(id);
         if (result is not null)
         {
            _context.Divisions.Remove(result);
            await _context.SaveChangesAsync();
            return result;
         }
         return null;
      }

      public async Task<Division> GetDivision(int id)
      {
         var result = await _context.Divisions.SingleOrDefaultAsync(d => d.DivisionId == id);
         return result;
      }

      public async Task<IEnumerable<Division>> GetDivisions()
      {
         var results = await _context.Divisions.ToListAsync();
         return results;
      }

      public async Task<Division> UpdateDivision(int id, UpdateDivisionDto updateDivisionDto)
      {
         var name = updateDivisionDto.Name.ToLower().Trim();
         var exist = await _context.Divisions.SingleOrDefaultAsync(d => d.Name.ToLower() == name);
         if (exist is not null)
         {
            throw new ArgumentException("Name already exist");
         }
         else if (name == "division")
         {
            throw new ArgumentException("Invalid name");
         }
         else
         {
            var result = await _context.Divisions.FindAsync(id);
            if (result is not null)
            {
               result.Name = updateDivisionDto.Name;
               await _context.SaveChangesAsync();
               return result;
            }

            return null;
         }
      }
   }
}