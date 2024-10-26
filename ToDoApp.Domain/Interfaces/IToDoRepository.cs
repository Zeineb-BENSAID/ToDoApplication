using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces
{
    public interface IToDoRepository
    {
        Task<ToDo> GetByIdAsync(int id);
        Task<IEnumerable<ToDo>> GetAllAsync();
        Task AddAsync(ToDo todo);
        Task UpdateAsync(ToDo todo);
        Task DeleteAsync(int id);
    }
}
