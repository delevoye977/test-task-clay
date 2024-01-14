using ClayDoorsProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClayDoorsProject.Repositories
{
    /// <summary>
    /// Context to access the claydoor database.
    /// </summary>
    public class ClayDoorDatabaseContext : DbContext
    {
        private readonly string connectionString;

        public ClayDoorDatabaseContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ClaydoorDatabase") ?? "";
        }

        public DbSet<DoorEntity> Doors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
