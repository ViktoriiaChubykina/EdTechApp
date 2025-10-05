using Xunit;
using EdTechApp; // щоб бачити класи з основного проєкту
using System.Linq;
using EdTechApp.Models;
using EdTechApp.Services;


namespace EdTechApp.Tests
{
    public class StudentTests
    {
        [Fact] // атрибут означає "це тест"
        public void Student_Can_Enroll_In_Course()
        {
            // Arrange (підготовка)
            var student = new Student("Іван", "ivan@test.com", 20, "Група A");
            var teacher = new Teacher("Петро", "petro@test.com");
            var course = teacher.CreateCourse("Математика", "Алгебра і геометрія", true);

            // Act (дія)
            student.Enroll(course);

            // Assert (перевірка результату)
            Assert.Contains(course, student.EnrolledCourses);
        }

        [Fact]
        public void Assignment_Marked_As_Completed()
        {
            // Arrange
            var assignment = new Assignment("Домашка №1", "Розв’язати рівняння");

            // Act
            assignment.Complete();

            // Assert
            Assert.True(assignment.IsCompleted);
        }

        [Fact]
        public void Student_Progress_Is_Calculated_Correctly()
        {
            var student = new Student("Марія", "maria@test.com", 19, "Група B");
            var teacher = new Teacher("Олена", "olena@test.com");
            var course = teacher.CreateCourse("Програмування", "C# базовий", true);

            var assignment1 = new Assignment("Завдання 1", "опис 1");
            var assignment2 = new Assignment("Завдання 2", "опис 2");
            course.AddAssignment(assignment1);
            course.AddAssignment(assignment2);

            student.Enroll(course);

            assignment1.Complete();

            int done = course.Assignments.Count(a => a.IsCompleted);
            int total = course.Assignments.Count;

            Assert.Equal(1, done);
            Assert.Equal(2, total);
        }

        [Fact]
        public void Student_Data_Can_Be_Updated()
        {
            var student = new Student("Іван", "ivan@test.com", 18, "Група А");

            student.Name = "Іванко";
            student.Age = 19;
            student.IsActive = false;

            Assert.Equal("Іванко", student.Name);
            Assert.Equal(19, student.Age);
            Assert.False(student.IsActive);
        }
        [Fact]
        public void GetActiveStudents_Returns_Only_Active_Students()
        {
            var service = new StudentService();
            var student1 = new Student("Іван", "ivan@test.com", 18, "Група А", true);
            var student2 = new Student("Марія", "maria@test.com", 19, "Група B", false);
            service.AddStudent(student1);
            service.AddStudent(student2);

            var activeStudents = service.GetActiveStudents();

            Assert.Contains(student1, activeStudents);
            Assert.DoesNotContain(student2, activeStudents);
        }

    }
}
