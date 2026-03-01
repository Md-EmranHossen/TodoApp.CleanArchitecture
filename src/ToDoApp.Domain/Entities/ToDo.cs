using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Entities
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a due date.")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; } = string.Empty;

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        public string StatusId { get; set; } = string.Empty;

        public Status? Status { get; set; }

        public bool Overdue => StatusId == "open" && DueDate.HasValue && DueDate.Value.Date < DateTime.Today;
    }
}
