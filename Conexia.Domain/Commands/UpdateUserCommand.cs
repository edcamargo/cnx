using Conexia.Domain.Shared.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Conexia.Domain.Commands
{
    public class UpdateUserCommand : Notifiable, ICommand
    {
        public UpdateUserCommand() { }

        public UpdateUserCommand(Guid id, string name, string email, string city, string personalNotes)
        {
            Id = id;
            Name = name;
            Email = email;
            City = city;
            PersonalNotes = personalNotes;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PersonalNotes { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotEmpty(Id, "Id", "id inválido")
                    .HasMinLen(Name, 3, "Nome", "nome inválido")
                    .IsEmail(Email, "Email", "Por favor, e-mail inválido!")
                    .HasMinLen(City, 3, "Cidade", "Cidade inválido")
                    .HasMinLen(PersonalNotes, 5, "Notas pessoais", "Notas Pessoais inválido")
            );
        }
    }
}
