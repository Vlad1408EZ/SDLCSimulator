using Microsoft.EntityFrameworkCore;

namespace SDLCSimulator_Data.Extensions
{
    public static class AddRelations
    {
        public static void AddDbRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.TaskResults)
                .WithOne(tr => tr.Student)
                .HasForeignKey(tr => tr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GroupTeachers)
                .WithOne(gt => gt.Teacher)
                .HasForeignKey(gt => gt.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.Teacher)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Group)
                .WithMany()
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.GroupTasks)
                .WithOne(gt => gt.Task)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.TaskResults)
                .WithOne(tr => tr.Task)
                .HasForeignKey(tr => tr.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.GroupTasks)
                .WithOne(gt => gt.Group)
                .HasForeignKey(gt => gt.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.GroupTeachers)
                .WithOne(gt => gt.Group)
                .HasForeignKey(gt => gt.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
