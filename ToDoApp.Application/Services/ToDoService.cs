using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services
{
    public class ToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDo>> GetAllToDosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddToDoAsync(string name)
        {
            var todo = new ToDo
            {
                Name = name,
                CreatedAt = DateTime.Now,
                Status = false
            };
            await _repository.AddAsync(todo);
        }

        public async Task MarkToDoAsCompleteAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo != null)
            {
                todo.MarkAsComplete();
                await _repository.UpdateAsync(todo);
            }
        }

        public async Task DeleteToDoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<string> GetMostProductiveDayAsync()
        {
            // Get all completed tasks from the database
            var completedTasks = await _repository.GetAllAsync();
            var completedOnDays = completedTasks
                .Where(t => t.Status) // Only completed tasks
                .GroupBy(t => t.CompletedAt?.DayOfWeek) // Group by the day of the week
                .Select(g => new { DayOfWeek = g.Key, Count = g.Count() }) // Get the count per day
                .OrderByDescending(g => g.Count) // Order by the count in descending order
                .FirstOrDefault();

            return completedOnDays?.DayOfWeek?.ToString() ?? "No data available";
        }
    }
}
