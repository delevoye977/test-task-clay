using ClayDoorsDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ClayDoorsDatabase.Repositories
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

        internal DbSet<DoorEntity> Doors { get; set; }
        internal DbSet<DoorUserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoorEntity>()
                .HasMany(e => e.Permissions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "door_permission_link",
                    j => j.HasOne<DoorPermissionEntity>().WithMany().HasForeignKey("permission_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<DoorEntity>().WithMany().HasForeignKey("door_id").OnDelete(DeleteBehavior.NoAction)
                    );

            modelBuilder.Entity<DoorUserEntity>()
                .HasMany(e => e.Roles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "user_role_link",
                    j => j.HasOne<UserRoleEntity>().WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<DoorUserEntity>().WithMany().HasForeignKey("user_id").OnDelete(DeleteBehavior.NoAction)
                    );

            modelBuilder.Entity<UserRoleEntity>()
                .HasMany(e => e.Permissions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "role_permission_link",
                    j => j.HasOne<DoorPermissionEntity>().WithMany().HasForeignKey("permission_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<UserRoleEntity>().WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.NoAction)
                );
        }
    }
}
