using System.Data.Entity;
using SharedLibrary.Entities;

namespace DAL.Repository.EntityFramework
{
    public class EfDbContext : DbContext
    {
        public EfDbContext()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Group>().ToTable("Group");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}