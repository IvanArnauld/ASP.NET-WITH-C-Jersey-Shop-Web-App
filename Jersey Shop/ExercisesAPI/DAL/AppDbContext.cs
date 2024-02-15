using Microsoft.EntityFrameworkCore;
using ExercisesAPI.DAL.DomainClasses;

namespace ExercisesAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<MenuItem>? MenuItems { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Tray>? Trays { get; set; }
        public virtual DbSet<TrayItem>? TrayItems { get; set; }
        public virtual DbSet<Store>? Stores { get; set; }
    }
}
