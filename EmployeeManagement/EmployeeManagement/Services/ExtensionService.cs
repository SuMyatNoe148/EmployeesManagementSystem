using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public interface IExtensionService
    {
        Task<string> GenerateEmployeeNumber();
    }

    public class ExtensionService : IExtensionService
    {
        private readonly ApplicationDbContext _context;
        public ExtensionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateEmployeeNumber()
        {
            string employeeNumber;
            bool exists;
            Random _randomizer = new Random();

            do
            {
                int randomNumber = _randomizer.Next(10000, 99999);
                employeeNumber = $"EMP{randomNumber}";

                //check if it exists
                exists = await _context.Employees.AnyAsync(e => e.EmpNo == employeeNumber);

            } while (exists);
            return employeeNumber;
        }
    }
}
