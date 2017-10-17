using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.StudentSystemDb
{
    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer("Server=.;Database=StudentSystemDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<Student>()
                .HasMany(st => st.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .Entity<Student>()
                .HasMany(st => st.Homeworks)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId);

            builder
                .Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId);

            builder
                .Entity<Resource>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);

            builder
                .Entity<Homework>()
                .HasOne(h => h.Course)
                .WithMany(c => c.Homeworks)
                .HasForeignKey(h => h.CourseId);
        }
    }
}
