namespace EdTechApp.Models
{// Course.cs
    public class Course
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Teacher Creator { get; set; }
        public bool InclusiveSupport { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();

        public Course(string title, string description, Teacher creator, bool inclusiveSupport)
        {
            Title = title;
            Description = description;
            Creator = creator;
            InclusiveSupport = inclusiveSupport;
        }


        public void AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
            Logger.WriteLine($"До курсу {Title} додано завдання: {assignment.Title}");
        }

        public void ShowInfo()
        {
            Logger.WriteLine($"Курс: {Title} — {Description}");
            Logger.WriteLine($"Підтримка інклюзивності: {(InclusiveSupport ? "Так" : "Ні")}");

            if (Assignments.Any())
            {
                Logger.WriteLine("Завдання курсу:");
                foreach (var a in Assignments)
                    Logger.WriteLine($" - {a.Title}");
            }
            else
            {
                Logger.WriteLine("Завдання відсутні.");
            }

            if (Materials.Any())
            {
                Logger.WriteLine("Матеріали курсу:");
                foreach (var mat in Materials)
                    Logger.WriteLine($" - {mat.Title} ({mat.Type})");
            }
            else
            {
                Logger.WriteLine("Матеріали курсу відсутні.");
            }
        }

        public List<Material> Materials { get; set; } = new List<Material>();

        public void AddMaterial(Material material)
        {
            Materials.Add(material);
            Logger.WriteLine($"До курсу {Title} додано матеріал: {material.Title}");
        }
    }
}