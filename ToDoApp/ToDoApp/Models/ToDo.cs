namespace ToDoApp.Models
{
    public class ToDo
    {
        public string? ToDoHeading { get; set; }
        public string? ToDoDescription { get; set; }
        public DateTime TargetDate { get; set; }
        public TaskRecurrence ToDoRecurrence { get; set; }

        public ToDo(string? toDoHeading, string? toDoDescription, DateTime targetDate, TaskRecurrence toDoRecurrence)
        {
            ToDoHeading = toDoHeading;
            ToDoDescription = toDoDescription;
            TargetDate = targetDate;
            ToDoRecurrence = toDoRecurrence;
        }
    }
    public enum TaskRecurrence
    {
        None = 0,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
