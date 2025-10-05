namespace EdTechApp.Models
{// User.cs
    public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public abstract void ShowInfo();
    }
}