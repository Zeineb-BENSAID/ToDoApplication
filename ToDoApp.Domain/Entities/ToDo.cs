using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool Status { get; set; } // true = completed, false = pending

        public void MarkAsComplete()
        {
            Status = true;// if completed
            CompletedAt = DateTime.Now;
        }
    }
}
