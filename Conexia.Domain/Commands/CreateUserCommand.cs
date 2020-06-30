using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Conexia.Domain.Commands
{
    public class CreateUserCommand : Notifiable, ICommand
    {
        public CreateUserCommand() { }

        public CreateUserCommand(string name, string email, string password, string city, string personalNotes)
        {
            Name = name;
            Email = email;
            Password = password;
            City = city;
            PersonalNotes = personalNotes;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }

        public string PersonalNotes { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 3, "Nome", "nome inválido")
                    .IsEmail(Email, "Email", "Por favor, e-mail inválido!")
                    .HasMinLen(Password, 6, "Senha", "Password inválido, minimo 6 caracteres")
                    .HasMinLen(City, 3, "Cidade", "Cidade inválido")
                    .HasMinLen(PersonalNotes, 5, "Notas pessoais", "Notas Pessoais inválido")
            );
        }
    }
}
