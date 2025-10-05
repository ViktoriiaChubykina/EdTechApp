namespace EdTechApp.Models
{// Teacher.cs
    public class Teacher : User
    {
        public List<Course> CreatedCourses { get; set; } = new List<Course>();

        public Teacher(string name, string email) : base(name, email) { }

        public Course CreateCourse(string title, string description, bool inclusiveSupport)
        {
            var course = new Course(title, description, this, inclusiveSupport);
            CreatedCourses.Add(course);
            Logger.WriteLine($"{Name} створив курс: {course.Title}");
            return course;
        }

        public override void ShowInfo()
        {
            Logger.WriteLine($"Викладач: {Name}, Email: {Email}, Створено курсів: {CreatedCourses.Count}");
        }
    }
}