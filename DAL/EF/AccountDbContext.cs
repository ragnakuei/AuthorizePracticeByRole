using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasKey(gr => new
                                      {
                                          gr.Id
                                      });
            
            modelBuilder.Entity<UserGroup>()
                        .HasKey(gr => new
                                      {
                                          gr.GroupId,
                                          gr.UserId
                                      });
            
            modelBuilder.Entity<GroupRole>()
                        .HasKey(gr => new
                         {
                             gr.GroupId,
                             gr.RoleId
                         });
            
            modelBuilder.Entity<Role>()
                        .HasKey(gr => new
                                      {
                                          gr.Id
                                      });
            
            modelBuilder.Entity<RouteRole>()
                        .HasKey(gr => new
                         {
                             gr.RouteId,
                             gr.RoleId
                         });
            
            modelBuilder.Entity<AuthResultDto>()
                        .HasKey(dto => new
                         {
                             dto.UserId
                         });
        }

        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupRole> GroupRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<RouteRole> RouteRole { get; set; }
        public virtual DbSet<AuthResultDto> AuthResultDtos { get; set; }
    }
}