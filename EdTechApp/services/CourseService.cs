using EdTechApp.Models;
using EdTechApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdTechApp.Services
{
    public class CourseService
    {
        private List<Course> courses = new List<Course>();

        public void AddCourse(Course course)
        {
            if (courses.Any(c => c.Title == course.Title))
                throw new ArgumentException("Курс з такою назвою вже існує.");
            courses.Add(course);
        }

        public List<Course> GetAllCourses() => courses;

        public Course? GetCourseByTitle(string? title) =>
            courses.FirstOrDefault(c => c.Title == title);

        public void UpdateCourse(string title, string? newTitle = null, string? newDesc = null, bool? newInclusive = null)
        {
            var courseToUpdate = GetCourseByTitle(title);
            if (courseToUpdate == null) return;

            if (!string.IsNullOrWhiteSpace(newTitle)) courseToUpdate.Title = newTitle;
            if (!string.IsNullOrWhiteSpace(newDesc)) courseToUpdate.Description = newDesc;
            if (newInclusive.HasValue) courseToUpdate.InclusiveSupport = newInclusive.Value;
        }

        public void DeleteCourse(string? title)
        {
            var courseToDelete = GetCourseByTitle(title);
            if (courseToDelete  != null) 
            {courses.Remove(courseToDelete);}
        }

        public List<Course> GetInclusiveCourses()
        {
            return courses.Where(c => c.InclusiveSupport).ToList();
        }

    }
}