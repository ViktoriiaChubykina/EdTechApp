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
    }
}