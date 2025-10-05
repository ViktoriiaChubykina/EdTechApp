using Xunit;
using EdTechApp;
using EdTechApp.Models;
using EdTechApp.Services;
using System.Linq;

namespace EdTechApp.Tests
{
    public class IntegrationTests
    {
        public IntegrationTests()
        {
            Logger.EnableConsole = false;
        }
        [Fact]
        public void Student_Can_Enroll_And_Complete_Assignment()
        {
            // Arrange
            var teacher = new Teacher("Олена", "olena@test.com");
            var course = teacher.CreateCourse("Програмування", "Основи C#", true);
            var student = new Student("Марія", "maria@test.com", 18, "А1", true);

            var assignment = new Assignment("Лабораторна 1", "Написати Hello World");
            course.AddAssignment(assignment);

            // Act
            student.Enroll(course);
            assignment.Complete();

            // Assert
            Assert.Contains(course, student.EnrolledCourses);
            Assert.True(assignment.IsCompleted);
        }

        [Fact]
        public void Multiple_Students_Can_Enroll_And_Track_Progress()
        {
            // Arrange
            var teacher = new Teacher("Петро", "petro@test.com");
            var course = teacher.CreateCourse("Математика", "Алгебра", true);

            var student1 = new Student("Іван", "ivan@test.com", 19, "B2", true);
            var student2 = new Student("Марія", "maria@test.com", 18, "A1", true);

            var assignment1 = new Assignment("Завдання 1", "опис 1");
            var assignment2 = new Assignment("Завдання 2", "опис 2");
            course.AddAssignment(assignment1);
            course.AddAssignment(assignment2);

            // Act
            student1.Enroll(course);
            student2.Enroll(course);
            assignment1.Complete(); // тільки перше завдання виконано

            // Assert
            // Перевірка, що обидва студенти записані на курс
            Assert.Contains(course, student1.EnrolledCourses);
            Assert.Contains(course, student2.EnrolledCourses);

            // Перевірка прогресу студента1
            int done1 = course.Assignments.Count(a => a.IsCompleted);
            int total1 = course.Assignments.Count;
            Assert.Equal(1, done1);
            Assert.Equal(2, total1);
        }

        [Fact]
        public void Student_Progress_Updates_When_Assignment_Completed()
        {
            // Arrange
            var teacher = new Teacher("Олена", "olena@test.com");
            var course = teacher.CreateCourse("Програмування", "C# базовий", true);
            var student = new Student("Марія", "maria@test.com", 18, "A1", true);

            var assignment = new Assignment("Завдання 1", "опис");
            course.AddAssignment(assignment);

            student.Enroll(course);

            // Act
            assignment.Complete();

            // Assert
            int done = course.Assignments.Count(a => a.IsCompleted);
            int total = course.Assignments.Count;

            Assert.Equal(1, done);
            Assert.Equal(1, total);
        }

        [Fact]
        public void Course_With_Materials_And_Assignments_Shows_Correct_Info()
        {
            // Arrange
            var teacher = new Teacher("Олександр", "oleks@test.com");
            var course = teacher.CreateCourse("Фізика", "Механіка", true);

            var assignment = new Assignment("Лабораторна 1", "опис");
            var material = new Material("Відео 1", "Відео");
            course.AddAssignment(assignment);
            course.AddMaterial(material);

            // Capture console output
            using var sw = new StringWriter();
            Logger.SetWriter(sw);

            // Act
            course.ShowInfo();

            // Assert
            var output = sw.ToString();
            Assert.Contains("Фізика", output);
            Assert.Contains("Механіка", output);
            Assert.Contains("Лабораторна 1", output);
            Assert.Contains("Відео 1", output);

            Logger.SetWriter(Console.Out);
        }
    }
}