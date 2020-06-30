namespace Conexia.Domain.Entities
{
    public class User : Entity
    {
        public User(string name, string email, string password, string city, string personalNotes)
        {
            Name = name;
            Email = email;
            Password = password;
            City = city;
            PersonalNotes = personalNotes;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string City { get; private set; }
        public string PersonalNotes { get; private set; }

        public void UpdateUser(string name, string email, string city, string personalNotes)
        {
            Name = name;
            Email = email;
            City = city;
            PersonalNotes = personalNotes;
        }

        public bool UpdatePassword(string password)
        {
            Password = password; 
            return true;
        }
    }
}
