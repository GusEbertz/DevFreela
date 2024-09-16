using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
  public class DevFreelaDBContext : DbContext
  {
        public DevFreelaDBContext(DbContextOptions<DevFreelaDBContext> options)
      :base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get;  set; }
        public DbSet<Skill> Skills { get;  set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }
    public object HasForeingKey { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder
        .Entity<Skill>(e =>
        {
          e.HasKey(s => s.Id);
        });

      modelBuilder
        .Entity<User>(e =>
        {
          e.HasKey(u => u.Id);
          e.HasMany(u => u.Skills)
            .WithOne(us => us.User)
            .HasForeignKey(us => us.IdUser)
            .OnDelete(DeleteBehavior.Restrict);
        });

      modelBuilder
        .Entity<UserSkill>(e =>
        {
          e.HasKey(us => us.Id);
          e.HasOne(u => u.Skill)
          .WithMany(u => u.UserSkills)
          .HasForeignKey(s => s.IdSkill)
          .OnDelete(DeleteBehavior.Restrict);
        });

      modelBuilder
        .Entity<Project>(e =>
        {
          e.HasKey(p => p.Id);
          e.HasOne(p => p.Freelancer)
          .WithMany(f => f.FreelanceProjects)
          .HasForeignKey(p => p.IdFreelancer)
          .OnDelete(DeleteBehavior.Restrict);

          e.Property(p => p.TotalCost).HasPrecision(18, 2);

          e.HasOne(p => p.Client)
          .WithMany(c => c.OwnedProjects)
          .HasForeignKey(p => p.IdClient)
          .OnDelete(DeleteBehavior.Restrict);

        });

      

      modelBuilder
        .Entity<ProjectComment>(e =>
        {
          e.HasKey(p => p.Id);
          e.HasOne(p => p.Project)
          .WithMany(p => p.Comments)
          .HasForeignKey(p => p.IdProjeto)
          .OnDelete(DeleteBehavior.Restrict);
        });

      

      base.OnModelCreating(modelBuilder);
    }
  }
}
