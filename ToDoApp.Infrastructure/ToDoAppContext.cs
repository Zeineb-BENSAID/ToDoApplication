using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure
{
    public class ToDoAppContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=todo.db;");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                      Initial Catalog=todo_db;
                                      Integrated Security=true;
                                      MultipleActiveResultSets=true");//multiple data readers
            optionsBuilder.UseLazyLoadingProxies(true);//activer lazy loading
            base.OnConfiguring(optionsBuilder);
        }
    }
}
