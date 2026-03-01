using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public List<ToDo> GetTasks()
        {
            return _context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status)
                .ToList();
        }

        public List<Category> GetCategories() => _context.Categories.ToList();

        public List<Status> GetStatuses() => _context.Statues.ToList();

        public void AddTask(ToDo task)
        {
            _context.ToDos.Add(task);
            _context.SaveChanges();
        }

        public void MarkComplete(int id)
        {
            var task = _context.ToDos.Find(id);
            if (task != null)
            {
                task.StatusId = "closed";
                _context.SaveChanges();
            }
        }

        public void DeleteCompleted()
        {
            var toDelete = _context.ToDos.Where(t => t.StatusId == "closed").ToList();
            if (toDelete.Count == 0)
            {
                return;
            }

            _context.ToDos.RemoveRange(toDelete);
            _context.SaveChanges();
        }
    }
}
