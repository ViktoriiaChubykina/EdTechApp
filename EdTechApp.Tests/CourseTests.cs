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

        [Fact]
        public void Course_Can_Add_Material()
        {
            // Arrange
            var teacher = new Teacher("Петро", "petro@test.com");
            var course = teacher.CreateCourse("Фізика", "Механіка", true);
            var material = new Material("Відео 1", "Відео");

            // Act
            course.AddMaterial(material);

            // Assert
            Assert.Contains(material, course.Materials);
        }

        [Fact]
        public void UpdateCourse_Changes_Applied()
        {
            // Arrange
            var teacher = new Teacher("Марія", "maria@test.com");
            var course = teacher.CreateCourse("Біологія", "Загальна біологія", true);

            // Act
            course.Title = "Біологія 2";
            course.Description = "Розширена біологія";
            course.InclusiveSupport = false;

            // Assert
            Assert.Equal("Біологія 2", course.Title);
            Assert.Equal("Розширена біологія", course.Description);
            Assert.False(course.InclusiveSupport);
        }

        [Fact]
        public void DeleteCourse_Removes_Course()
        {
            // Arrange
            var teacher = new Teacher("Іван", "ivan@test.com");
            var course = teacher.CreateCourse("Хімія", "Основи хімії", true);
            var service = new CourseService();
            service.AddCourse(course);

            // Act
            service.DeleteCourse(course.Title);

            // Assert
            Assert.DoesNotContain(course, service.GetAllCourses());
        }

        [Fact]
        public void GetInclusiveCourses_Returns_Only_Inclusive()
        {
            // Arrange
            var teacher = new Teacher("Олена", "olena@test.com");
            var course1 = teacher.CreateCourse("Географія", "Фізична географія", true);
            var course2 = teacher.CreateCourse("Історія", "Всесвітня історія", false);
            var service = new CourseService();
            service.AddCourse(course1);
            service.AddCourse(course2);

            // Act
            var inclusiveCourses = service.GetInclusiveCourses();

            // Assert
            Assert.Contains(course1, inclusiveCourses);
            Assert.DoesNotContain(course2, inclusiveCourses);
        }
    }
}