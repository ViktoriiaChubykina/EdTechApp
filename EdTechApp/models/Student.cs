namespace EdTechApp.Models
{// Student.cs
    public class Student : User
    {
        public int Age { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        public Student(string name, string email, int age, string group, bool isActive = true) : base(name, email) 
        { 
            Age = age;
            Group = group;
            IsActive = isActive;
        }

        public void Enroll(Course course)
        {
            EnrolledCourses.Add(course);
            Logger.WriteLine($"{Name} приєднався до курсу: {course.Title}");
        }

        public override void ShowInfo()
        {
            Logger.WriteLine($"Студент: {Name}, Email: {Email}, Вік: {Age}, " +
                              $"Група: {Group}, Статус: {(IsActive ? "Активний" : "Неактивний")}, " +
                              $"Кількість курсів: {EnrolledCourses.Count}");
        }
        
        public void ShowProgress()
        {
            foreach (var course in EnrolledCourses)
            {
                int total = course.Assignments.Count;
                int done = course.Assignments.Count(a => a.IsCompleted);
                Logger.WriteLine($"Курс: {course.Title} — виконано {done}/{total} завдань");
            }
        }
    }
}