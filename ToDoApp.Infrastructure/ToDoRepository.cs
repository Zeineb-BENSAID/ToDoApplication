using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastructure
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoAppContext _context;

        public ToDoRepository(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<ToDo> GetByIdAsync(int id)
        {
            return await _context.ToDos.FindAsync(id);
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            try
            {
                var todos = await _context.ToDos.ToListAsync();
                return todos ?? new List<ToDo>();
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to console or a logging framework)
                Console.WriteLine($"Error fetching todos: {ex.Message}");
                return new List<ToDo>();
            };

        }

        public async Task AddAsync(ToDo todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo != null)
            {
                _context.ToDos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
