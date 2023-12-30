using MVC_Project.BLL.Interfaces;
using MVC_Project.DAL.Contexts;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
    public class EmployeeRepository:GenericRepository<Employee> ,IEmployeeRepository 
    {
       
        public EmployeeRepository(MVCDbContext dbContext):base (dbContext)
        {
            
            
        }

        

        public IQueryable<Employee> GetEmployeesByAddress(string aaddress)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchEmployeesByName(string name)
         => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
    }
}
