using ManageEmployees.Data;
using ManageEmployees.Entities;
using ManageEmployees.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}