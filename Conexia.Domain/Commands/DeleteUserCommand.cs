using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Conexia.Domain.Commands
{
    public class DeleteUserCommand : Notifiable, ICommand
    {
        public DeleteUserCommand() { }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotEmpty(Id, "Id", "Por favor, Id inválido!")
            );
        }
    }
}
