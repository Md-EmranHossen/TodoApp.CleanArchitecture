using System.Collections.Generic;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Interfaces
{
    public interface IToDoRepository
    {
        List<ToDo> GetTasks();
        List<Category> GetCategories();
        List<Status> GetStatuses();
        void AddTask(ToDo task);
        void MarkComplete(int id);
        void DeleteCompleted();
    }
}
