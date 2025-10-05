using EdTechApp.Models;

namespace EdTechApp.Services
{
    public class StudentService
    {
        private readonly List<Student> _students = new List<Student>();

        // Додати нового студента з валідацією
        public void AddStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Ім'я не може бути пустим.");

            if (!student.Email.Contains("@"))
                throw new ArgumentException("Некоректний email.");

            if (student.Age < 16 || student.Age > 100)
                throw new ArgumentException("Вік має бути від 16 до 100.");

            if (_students.Any(s => s.Email == student.Email))
                throw new ArgumentException("Студент з таким email вже існує.");

            _students.Add(student);
            Console.WriteLine($"✅ Студента {student.Name} додано.");
        }

        // Отримати список усіх студентів
        public List<Student> GetAllStudents()
        {
            return _students;
        }

        // Пошук за email
        public Student? GetStudentByEmail(string email)
        {
            return _students.FirstOrDefault(s => s.Email == email);
        }

        // Оновити студента
        public void UpdateStudent(string email, string? newName = null, int? newAge = null, string? newGroup = null, bool? newStatus = null)
        {
            var student = GetStudentByEmail(email);
            if (student == null)
            {
                Console.WriteLine("Студента не знайдено.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(newName)) student.Name = newName;
            if (newAge.HasValue) student.Age = newAge.Value;
            if (!string.IsNullOrWhiteSpace(newGroup)) student.Group = newGroup;
            if (newStatus.HasValue) student.IsActive = newStatus.Value;

            Console.WriteLine($"✏️ Дані студента {student.Email} оновлено.");
        }

        // Видалити студента
        public void DeleteStudent(string email)
        {
            var student = GetStudentByEmail(email);
            if (student == null)
            {
                Console.WriteLine("Студента не знайдено.");
                return;
            }

            _students.Remove(student);
            Console.WriteLine($"🗑️ Студента {student.Name} видалено.");
        }

        // Фільтрація активних студентів
        public List<Student> GetActiveStudents()
        {
            return _students.Where(s => s.IsActive).ToList();
        }

        public void ShowAllProgress()
        {
            foreach (var student in _students)
                student.ShowProgress();
        }

    }
}