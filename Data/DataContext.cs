using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Batch> Batch => Set<Batch>();
        public DbSet<Course> Course => Set<Course>();
        public DbSet<Enrollment> Enrollment => Set<Enrollment>();
        public DbSet<Syllabus> Syllabus => Set<Syllabus>();
        public DbSet<Student> Student => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
            .HasOne(b => b.Batch)
            .WithMany(e => e.Enrollments);

            modelBuilder.Entity<Enrollment>()
            .HasOne(c => c.Course)
            .WithMany(e => e.Enrollments);

            modelBuilder.Entity<Enrollment>()
            .HasOne(s => s.Syllabus)
            .WithOne(b => b.Enrollment)
            .HasForeignKey<Syllabus>(e => e.EnrollmentID);
        }
    }
}