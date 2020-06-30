using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Conexia.Domain.Commands
{
    public class ForgotPasswordCommand : Notifiable, ICommand
    {
        public ForgotPasswordCommand() { }

        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsEmail(Email, "Email", "Por favor, e-mail inválido!")
            );
        }
    }
}
