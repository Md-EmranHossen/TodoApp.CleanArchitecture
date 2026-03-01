namespace ToDoApp.Application.Models
{
    public class ToDoQuery
    {
        public string CategoryId { get; set; } = "all";
        public string Due { get; set; } = "all";
        public string StatusId { get; set; } = "all";
    }
}
