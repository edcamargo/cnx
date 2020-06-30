using System;
using System.ComponentModel.DataAnnotations;

namespace Conexia.InfraStructure.AntiCorruption.Spotify.Entities
{
    public class Entity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
