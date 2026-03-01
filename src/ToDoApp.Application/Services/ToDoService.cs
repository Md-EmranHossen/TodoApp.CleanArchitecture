using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Models;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public List<ToDo> GetTasks(ToDoQuery query)
        {
            var tasks = _repository.GetTasks().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CategoryId) &&
                !string.Equals(query.CategoryId, "all", StringComparison.OrdinalIgnoreCase))
            {
                tasks = tasks.Where(t => t.CategoryId == query.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(query.StatusId) &&
                !string.Equals(query.StatusId, "all", StringComparison.OrdinalIgnoreCase))
            {
                tasks = tasks.Where(t => t.StatusId == query.StatusId);
            }

            if (!string.IsNullOrWhiteSpace(query.Due) &&
                !string.Equals(query.Due, "all", StringComparison.OrdinalIgnoreCase))
            {
                var today = DateTime.Today;
                if (string.Equals(query.Due, "past", StringComparison.OrdinalIgnoreCase))
                {
                    tasks = tasks.Where(t => t.DueDate < today);
                }
                else if (string.Equals(query.Due, "future", StringComparison.OrdinalIgnoreCase))
                {
                    tasks = tasks.Where(t => t.DueDate < today);
                }
                else if (string.Equals(query.Due, "today", StringComparison.OrdinalIgnoreCase))
                {
                    tasks = tasks.Where(t => t.DueDate == today);
                }
            }

            return tasks.OrderBy(t => t.DueDate).ToList();
        }

        public List<Category> GetCategories() => _repository.GetCategories();
        public List<Status> GetStatuses() => _repository.GetStatuses();
        public void AddTask(ToDo task) => _repository.AddTask(task);
        public void MarkComplete(int id) => _repository.MarkComplete(id);
        public void DeleteCompleted() => _repository.DeleteCompleted();
    }
}
