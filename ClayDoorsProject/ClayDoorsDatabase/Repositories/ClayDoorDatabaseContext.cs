﻿using ClayDoorsDatabase.Entities;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<DoorEntity> Doors { get; set; }
        public DbSet<DoorUserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
