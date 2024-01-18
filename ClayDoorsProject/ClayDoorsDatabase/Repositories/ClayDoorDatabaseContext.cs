using ClayDoorsDatabase.Entities;
using ClayDoorsModel.Models.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClayDoorsDatabase.Repositories
{
    /// <summary>
    /// Context to access the claydoor database.
    /// </summary>
    public class ClayDoorDatabaseContext : DbContext, IDatabaseContext<DoorEntity, IDoor, int>
    {
        private readonly string connectionString;

        public ClayDoorDatabaseContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ClaydoorDatabase") ?? "";
        }


        private DbSet<DoorEntity> doors;
        public DbSet<DoorEntity> Doors 
        { 
            get => doors;
            set { doors = value; }
        }

        internal DbSet<DoorUserEntity> Users { get; set; }
        internal DbSet<DoorUnlockLogEntity> DoorUnlockLogs { get; set; }


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
                    j => j.HasOne<DoorUserPermissionEntity>().WithMany().HasForeignKey("permission_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<DoorEntity>().WithMany().HasForeignKey("door_id").OnDelete(DeleteBehavior.NoAction)
                    );

            modelBuilder.Entity<DoorUserEntity>()
                .HasMany(e => e.Roles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "user_role_link",
                    j => j.HasOne<DoorUserRoleEntity>().WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<DoorUserEntity>().WithMany().HasForeignKey("user_id").OnDelete(DeleteBehavior.NoAction)
                    );

            modelBuilder.Entity<DoorUserRoleEntity>()
                .HasMany(e => e.Permissions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "role_permission_link",
                    j => j.HasOne<DoorUserPermissionEntity>().WithMany().HasForeignKey("permission_id").OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<DoorUserRoleEntity>().WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.NoAction)
                );
        }

        public Task<int> SaveAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
