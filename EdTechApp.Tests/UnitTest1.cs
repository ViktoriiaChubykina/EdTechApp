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
    }
}
