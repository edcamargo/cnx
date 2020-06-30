using Conexia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conexia.InfraStructure.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.PersonalNotes).HasMaxLength(500).HasColumnType("varchar(500)").IsRequired();
            builder.Property(x => x.Password).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.City).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
        }
    }
}
