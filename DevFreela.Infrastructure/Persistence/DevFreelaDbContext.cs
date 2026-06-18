using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options)
           : base(options)
        {
        }

        public DbSet<ProjectComment> Comments => Set<ProjectComment>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<UserSkill> UserSkills => Set<UserSkill>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Tittle).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(4000).IsRequired();
                entity.Property(p => p.TotalCost).HasPrecision(18, 2);
                entity.HasMany(p => p.Comments).WithOne().HasForeignKey(c => c.IdProject);

                entity.HasData(
                    new
                    {
                        Id = 1,
                        Tittle = "Meu projeto ASP.NET Core 1",
                        Description = "Minha descricao de projeto 1",
                        IdClient = 1,
                        IdFreelancer = 2,
                        TotalCost = 10000m,
                        CreatedAt = createdAt,
                        StartedAt = default(DateTime),
                        FinishedAt = default(DateTime),
                        Status = ProjectStatusEnum.Created
                    },
                    new
                    {
                        Id = 2,
                        Tittle = "Meu projeto ASP.NET Core 2",
                        Description = "Minha descricao de projeto 2",
                        IdClient = 1,
                        IdFreelancer = 3,
                        TotalCost = 20000m,
                        CreatedAt = createdAt,
                        StartedAt = default(DateTime),
                        FinishedAt = default(DateTime),
                        Status = ProjectStatusEnum.Created
                    });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).HasMaxLength(200).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(200).IsRequired();
                entity.Ignore(u => u.Skills);
                entity.Ignore(u => u.OwnedProjects);
                entity.Ignore(u => u.FreelanceProjects);

                entity.HasData(
                    new
                    {
                        Id = 1,
                        FullName = "Agatha Almeida",
                        Email = "agathasantos@gmail.com",
                        BirthDate = new DateTime(2001, 3, 3),
                        CreatedAt = createdAt,
                        Active = true
                    },
                    new
                    {
                        Id = 2,
                        FullName = "Ester Mota",
                        Email = "esterMotta@gmail.com",
                        BirthDate = new DateTime(2000, 11, 23),
                        CreatedAt = createdAt,
                        Active = true
                    },
                    new
                    {
                        Id = 3,
                        FullName = "Justin Bieber",
                        Email = "justinbieber@gmail.com",
                        BirthDate = new DateTime(1999, 1, 3),
                        CreatedAt = createdAt,
                        Active = true
                    });
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Description).HasMaxLength(100).IsRequired();

                entity.HasData(
                    new { Id = 1, Description = ".NET Core", CreatedAt = createdAt },
                    new { Id = 2, Description = "Angular", CreatedAt = createdAt },
                    new { Id = 3, Description = "SQL", CreatedAt = createdAt });
            });

            modelBuilder.Entity<ProjectComment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Content).HasMaxLength(2000).IsRequired();
            });

            modelBuilder.Entity<UserSkill>(entity =>
            {
                entity.HasKey(us => us.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
