namespace EdTechApp.Models
{// Assignment.cs
    public class Assignment
    {
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; private set; }

        public Assignment(string title, string taskDescription)
        {
            Title = title;
            TaskDescription = taskDescription;
            IsCompleted = false;
        }

        public void Complete()
        {
            IsCompleted = true;
            Logger.WriteLine($"Завдання '{Title}' виконано!");
        }
    }
}