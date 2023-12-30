using Microsoft.EntityFrameworkCore;
using MVC_Project.BLL.Interfaces;
using MVC_Project.DAL.Contexts;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private protected readonly MVCDbContext _dbContext;
        public GenericRepository(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T item)
             =>await _dbContext.Set<T>().AddAsync(item);
           
        public void Delete(T item)
            => _dbContext.Set<T>().Remove(item);
            

        public async Task<IEnumerable<T>> GetALL()
        {
            if(typeof(T)==typeof(Employee))
                return  (IEnumerable<T>)await _dbContext.Employees.Include(E=>E.Department).ToListAsync();  
            else
                return await _dbContext.Set<T>().ToListAsync();
        }
           
        public async Task<T> Get(int id)
            => await _dbContext.Set<T>().FindAsync(id);


        public void Update(T item)
             =>_dbContext.Set<T>().Update(item);
          
    }
}
