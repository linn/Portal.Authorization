namespace Linn.Portal.Authorization.Persistence
{
    using System;

    using Linn.Common.Configuration;
    using Linn.Portal.Authorization.Domain;

    using Microsoft.EntityFrameworkCore;

    public class ServiceDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

        public DbSet<Association> Associations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subject>().HasKey(a => a.Sub);

            builder.Entity<Permission>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Permission>().HasKey(p => p.Id);

            builder.Entity<Privilege>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Privilege>().HasKey(p => p.Id);

            builder.Entity<Association>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Association>().HasKey(p => p.Id);
            builder.Entity<Association>().Property(e => e.AssociatedResource)
                .HasConversion(
                    v => v.ToString(),
                    v => new Uri(v, UriKind.RelativeOrAbsolute));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var databaseName = ConfigurationManager.Configuration["DATABASE_NAME"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];

            var port = ConfigurationManager.Configuration["DATABASE_PORT"];

            if (string.IsNullOrEmpty(port))
            {
                port = "5432";
            }

            optionsBuilder.UseNpgsql(
                $"User ID={userId};Password={password};Host={host};Database={databaseName};Port={port};Pooling=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
