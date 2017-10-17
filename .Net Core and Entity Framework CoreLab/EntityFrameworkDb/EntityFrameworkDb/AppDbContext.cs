using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDb
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<WorkSector> WorkSectors { get; set; }
        public object Select { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=TestDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasOne<Department>(emp => emp.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(emp => emp.DepartmentId);

            builder.Entity<Employee>()
                .HasOne(emp => emp.Manager)
                .WithMany(emp => emp.Slaves)
                .HasForeignKey(emp => emp.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<Student>()
                .HasMany(st => st.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);

            builder.Entity<Worker>()
                .HasOne(w => w.WorkSector)
                .WithMany(ws => ws.Workers)
                .HasForeignKey(w => w.WorkSectorId);
        }
    }
}
