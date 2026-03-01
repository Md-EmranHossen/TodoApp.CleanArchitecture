using System.Collections.Generic;
using ToDoApp.Application.Models;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Interfaces
{
    public interface IToDoService
    {
        List<ToDo> GetTasks(ToDoQuery query);
        List<Category> GetCategories();
        List<Status> GetStatuses();
        void AddTask(ToDo task);
        void MarkComplete(int id);
        void DeleteCompleted();
    }
}
