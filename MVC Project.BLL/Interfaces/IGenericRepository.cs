using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task <IEnumerable<T>> GetALL();
        Task<T> Get(int id);
        Task Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
