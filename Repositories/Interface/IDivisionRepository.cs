using IssueWebApp.Dtos.Division;
using IssueWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueWebApp.Repositories.Interface
{
   public interface IDivisionRepository
   {
      Task<IEnumerable<Division>> GetDivisions();

      Task<Division> GetDivision(int id);

      Task<Division> CreateDivision(Division division);

      Task<Division> UpdateDivision(int id, UpdateDivisionDto updateDivisionDto);

      Task<Division> DeleteDivision(int id);
   }
}