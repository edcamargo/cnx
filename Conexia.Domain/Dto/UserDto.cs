using System;

namespace Conexia.Domain.Dto
{
    public sealed class UserDto
    {
        public UserDto(Guid id, string name, string email, string city, string personalNotes, object temperature = null, object playList = null)
        {
            Id = id;
            Name = name;
            Email = email;
            City = city;
            PersonalNotes = personalNotes;
            Temperature = temperature;
            PlayList = playList;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PersonalNotes { get; set; }
        public object Temperature { get; set; }
        public object PlayList { get; set; }
    }
}
