﻿using EdTechApp.Models;
using EdTechApp.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EdTechApp
{
    class Program
    {
        static List<User> users = new List<User>();
        // static List<Course> courses = new List<Course>();
        static StudentService studentService = new StudentService();
        static CourseService courseService = new CourseService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== EdTech Система ===");
                Console.WriteLine("1. Модуль студентів");
                Console.WriteLine("2. Зареєструвати викладача");
                Console.WriteLine("3. Створити курс");
                Console.WriteLine("4. Переглянути курси");
                Console.WriteLine("5. Додати завдання до курсу");
                Console.WriteLine("6. Приєднатися до курсу (студент)");
                Console.WriteLine("7. Виконати завдання");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть дію: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": StudentMenu(); break;
                    case "2": RegisterTeacher(); break;
                    case "3": CreateCourse(); break;
                    case "4": ShowCourses(); break;
                    case "5": AddAssignment(); break;
                    case "6": EnrollStudent(); break;
                    case "7": CompleteAssignment(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }
        // === Меню для студентів ===
        static void StudentMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Модуль студентів ---");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Переглянути всіх студентів");
                Console.WriteLine("3. Знайти студента за email");
                Console.WriteLine("4. Оновити дані студента");
                Console.WriteLine("5. Видалити студента");
                Console.WriteLine("6. Показати лише активних студентів");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш вибір: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Ім'я: ");
                        string name = Console.ReadLine() ?? "";
                        Console.Write("Email: ");
                        string email = Console.ReadLine() ?? "";
                        Console.Write("Вік: ");
                        if (!int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.WriteLine("❌ Невірний формат віку!");
                            return;
                        }
                        // int age = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Група: ");
                        string group = Console.ReadLine() ?? "";

                        // var student = new Student(name, email) { Age = age };
                        // studentService.AddStudent(student);
                        var student = new Student(name, email, age, group);
                        try
                        {
                            studentService.AddStudent(student);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"❌ Помилка: {ex.Message}");
                        }
                        break;

                    case "2":
                        var students = studentService.GetAllStudents();
                        foreach (var s in students) s.ShowInfo();
                        break;

                    case "3":
                        Console.Write("Введіть email: ");
                        var st = studentService.GetStudentByEmail(Console.ReadLine());
                        if (st != null) st.ShowInfo();
                        else Console.WriteLine("Студента не знайдено.");
                        break;

                    case "4":
                        Console.Write("Введіть email студента: ");
                        string updEmail = Console.ReadLine();
                        Console.Write("Нове ім'я (або Enter): ");
                        string newName = Console.ReadLine();
                        Console.Write("Новий вік (або Enter): ");
                        string ageInput = Console.ReadLine();
                        int? newAge = string.IsNullOrWhiteSpace(ageInput) ? null : int.Parse(ageInput);
                        Console.Write("Новий статус (активний/неактивний, або Enter): ");
                        string statusInput = Console.ReadLine();
                        bool? newStatus = statusInput?.ToLower() == "активний" ? true :
                                          statusInput?.ToLower() == "неактивний" ? false : null;

                        studentService.UpdateStudent(updEmail, newName, newAge, null, newStatus);
                        break;

                    case "5":
                        Console.Write("Введіть email: ");
                        studentService.DeleteStudent(Console.ReadLine());
                        break;

                    case "6":
                        var active = studentService.GetActiveStudents();
                        foreach (var s in active) s.ShowInfo();
                        break;

                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        // static void RegisterStudent()
        // {
        //     Console.Write("Ім'я: ");
        //     string? name = Console.ReadLine();
        //     Console.Write("Email: ");
        //     string? email = Console.ReadLine();

        //     if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        //     {
        //         Console.WriteLine("Ім'я та email не можуть бути порожніми.");
        //         return;
        //     }

        //     var student = new Student(name, email);
        //     users.Add(student);
        //     Console.WriteLine("Студента зареєстровано!");
        // }

        static void RegisterTeacher()
        {
            Console.Write("Ім'я: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ім'я не може бути порожнім.");
                return;
            }

            Console.Write("Email: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email не може бути порожнім.");
                return;
            }

            var teacher = new Teacher(name, email);
            users.Add(teacher);
            Console.WriteLine("Викладача зареєстровано!");
        }

        static void CreateCourse()
        {
            var teacher = users.OfType<Teacher>().FirstOrDefault();
            if (teacher == null)
            {
                Console.WriteLine("Спочатку зареєструйте викладача!");
                return;
            }

            Console.Write("Назва курсу: ");
            string? title = Console.ReadLine();
            Console.Write("Опис курсу: ");
            string? description = Console.ReadLine();
            Console.Write("Підтримка інклюзивності (так/ні): ");
            string? inclusiveInput = Console.ReadLine();
            bool inclusive = inclusiveInput?.ToLower() == "так";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Назва та опис курсу не можуть бути порожніми.");
                return;
            }

            var course = teacher.CreateCourse(title, description, inclusive);
            try
            {
                courseService.AddCourse(course);
                Console.WriteLine("Курс створено!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Помилка: {ex.Message}");
            }
        }

        static void ShowCourses()
        {
            var allCourses = courseService.GetAllCourses();
            if (!allCourses.Any())
            {
                Console.WriteLine("Курсів немає.");
                return;
            }

            foreach (var course in allCourses)
            {
                Console.WriteLine($"Курс: {course.Title} — {course.Description} (Інклюзивний: {(course.InclusiveSupport ? "Так" : "Ні")})");
                Console.WriteLine("Матеріали курсу:");
                foreach (var mat in course.Materials)
                    Console.WriteLine($" - {mat.Title} ({mat.Type})");
                Console.WriteLine(); // порожній рядок між курсами
            }
        }

        static void AddAssignment()
        {
            if (!courseService.GetAllCourses().Any())
            {
                Console.WriteLine("Немає курсів.");
                return;
            }

            Console.Write("Введіть назву курсу: ");
            string? title = Console.ReadLine();
            var course = courseService.GetCourseByTitle(title);
            if (course == null)
            {
                Console.WriteLine("Курс не знайдено.");
                return;
            }

            Console.Write("Назва завдання: ");
            string? taskTitle = Console.ReadLine();
            Console.Write("Опис завдання: ");
            string? taskDesc = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(taskTitle) || string.IsNullOrWhiteSpace(taskDesc))
            {
                Console.WriteLine("Назва та опис завдання не можуть бути порожніми.");
                return;
            }

            var assignment = new Assignment(taskTitle, taskDesc);
            course.AddAssignment(assignment);
            Console.WriteLine("Завдання додано до курсу!");
        }

        static void EnrollStudent()
        {
            var student = studentService.GetAllStudents().FirstOrDefault();
            if (student == null)
            {
                Console.WriteLine("Спочатку зареєструйте студента!");
                return;
            }

            Console.Write("Введіть назву курсу: ");
            string? title = Console.ReadLine();
            var course = courseService.GetCourseByTitle(title);
            if (course == null)
            {
                Console.WriteLine("Курс не знайдено.");
                return;
            }

            student.Enroll(course);
            Console.WriteLine("Студент приєднався до курсу!");
        }

        static void CompleteAssignment()
        {
            Console.Write("Email студента: ");
            var student = studentService.GetStudentByEmail(Console.ReadLine());
            if (student == null)
            {
                Console.WriteLine("Спочатку зареєструйте студента!");
                return;
            }

            Console.Write("Назва курсу: ");
            string? courseTitle = Console.ReadLine();
            var course = student.EnrolledCourses.FirstOrDefault(c => c.Title == courseTitle);
            if (course == null)
            {
                Console.WriteLine("Студент не зареєстрований на цей курс.");
                return;
            }

            Console.Write("Назва завдання: ");
            string? taskTitle = Console.ReadLine();
            var assignment = course.Assignments.FirstOrDefault(a => a.Title == taskTitle);
            if (assignment == null)
            {
                Console.WriteLine("Завдання не знайдено.");
                return;
            }

            assignment.Complete();
                Console.WriteLine($"Студент {student.Name} виконав завдання: {assignment.Title}");
        }
    }
}