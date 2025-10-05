using Xunit;
using EdTechApp;
using EdTechApp.Models;
using EdTechApp.Services;
using System.Linq;

namespace EdTechApp.Tests
{
    public class CourseTests
    {
        [Fact]
        public void Teacher_Can_Create_Course()
        {
            // Arrange
            var teacher = new Teacher("Олена", "olena@test.com");

            // Act
            var course = teacher.CreateCourse("Програмування", "C# базовий", true);

            // Assert
            Assert.Equal("Програмування", course.Title);
            Assert.Equal("C# базовий", course.Description);
            Assert.True(course.InclusiveSupport);
            Assert.Equal(teacher, course.Creator);
        }

        [Fact]
        public void Course_Can_Add_Assignment()
        {
            // Arrange
            var teacher = new Teacher("Олександр", "oleks@test.com");
            var course = teacher.CreateCourse("Математика", "Алгебра", true);
            var assignment = new Assignment("Домашка 1", "Розв'язати рівняння");

            // Act
            course.AddAssignment(assignment);

            // Assert
            Assert.Contains(assignment, course.Assignments);
        }
    }
}