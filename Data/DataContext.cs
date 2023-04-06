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
        public DbSet<Subject> Subject => Set<Subject>();
        public DbSet<SubjectInSyllabus> SubjectInSyllabus => Set<SubjectInSyllabus>();
        public DbSet<Lecture> Lecture => Set<Lecture>();
        public DbSet<Title> Title => Set<Title>();
        public DbSet<Article> Article => Set<Article>();

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

            modelBuilder.Entity<SubjectInSyllabus>()
            .HasOne(s => s.Subject)
            .WithMany(ss => ss.SubjectInSyllabus);

            modelBuilder.Entity<SubjectInSyllabus>()
            .HasOne(s => s.Syllabus)
            .WithMany(ss => ss.SubjectInSyllabus);

            modelBuilder.Entity<Lecture>()
            .HasOne(s => s.SubjectInSyllabus)
            .WithOne(l => l.Lecture)
            .HasForeignKey<SubjectInSyllabus>(ss => ss.LectureID);
        }
    }
}