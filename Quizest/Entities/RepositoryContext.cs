using Entities.Configuration;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<QuizInfo> QuizInfos { get; set; }

        public DbSet<AnswerInfo> AnswerInfos { get; set; }

        public DbSet<TemporaryLink> TemporaryLinks { get; set; }

        public DbSet<GlobalSettings> GlobalSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizInfoUser>()
                .HasKey(qu => new { qu.QuizInfoId, qu.UserId });

            modelBuilder.Entity<TemporaryLink>()
                .HasIndex(t => t.Link).IsUnique();

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new QuizInfoUserConfiguration());
            modelBuilder.ApplyConfiguration(new QuizInfoConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerInfoConfiguration());
        }
    }
}
