using Conexia.Domain.Entities;
using Conexia.InfraStructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Conexia.InfraStructure.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Mappings
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
