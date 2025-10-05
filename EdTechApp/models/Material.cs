namespace EdTechApp.Models
{
    public class Material
    {
        public string Title { get; set; }
        public string Type { get; set; } // Наприклад: "Відео", "PDF", "Текст"

        public Material(string title, string type)
        {
            Title = title;
            Type = type;
        }
    }
}