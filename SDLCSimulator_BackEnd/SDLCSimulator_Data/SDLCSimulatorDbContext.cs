using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data.Extensions;

namespace SDLCSimulator_Data
{
    public class SDLCSimulatorDbContext : DbContext
    {

        public SDLCSimulatorDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupTask> GroupTasks { get; set; }
        public DbSet<GroupTeacher> GroupTeachers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskResult> TaskResults { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddDbRelations();
            modelBuilder.SetDbPrimaryKeys();
        }
    }
}
