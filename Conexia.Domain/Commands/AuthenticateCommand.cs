using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Conexia.Domain.Commands
{
    public class AuthenticateCommand : Notifiable, ICommand
    {
        public AuthenticateCommand(){ }

        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsEmail(Email, "Email", "Por favor, e-mail inválido!")
                    .HasMinLen(Password, 6, "Senha", "Password inválido, minimo 6 caracteres")
            );
        }
    }
}
