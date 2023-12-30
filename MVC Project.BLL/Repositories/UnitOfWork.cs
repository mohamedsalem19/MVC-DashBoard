using MVC_Project.BLL.Interfaces;
using MVC_Project.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get ; set; }

        public UnitOfWork(MVCDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext); 
            DepartmentRepository= new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
          => await _dbContext.SaveChangesAsync();

        public void Dispose()
        =>_dbContext.Dispose(); 
    }
}
