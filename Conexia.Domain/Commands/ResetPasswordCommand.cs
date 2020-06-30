using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Conexia.Domain.Commands
{
    public class ResetPasswordCommand : Notifiable, ICommand
    {
        public ResetPasswordCommand() { }

        public ResetPasswordCommand(string email, string password, string oldpassword)
        {
            Email = email;
            Password = password;
            Oldpassword = oldpassword;
        }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Oldpassword { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsEmail(Email, "Email", "Por favor, e-mail inválido!")
                    .HasMinLen(Password, 6, "Password", "Por favor, nova senha inválido!")
                    .HasMinLen(Oldpassword, 6, "Old Password", "Por favor, antiga senha inválido!")
            );
        }
    }
}
