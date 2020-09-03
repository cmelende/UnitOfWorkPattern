using System.Data.Entity;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ORMEntityFramework
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string server = "localhost";
            const string databaseName = "dew_db_test";
            const string userName = "dew_user";
            const string password = "testpw";
            optionsBuilder.UseMySQL($"server={server};database={databaseName};uid={userName};pwd={password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dew_db_test");
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("users");
                b.HasKey(t => t.Id);
                b.Property(t => t.FirstName).HasColumnName("first_name");
                b.Property(t => t.MiddleName).HasColumnName("middle_name");
                b.Property(t => t.LastName).HasColumnName("last_name");
            });
        }
    }
}