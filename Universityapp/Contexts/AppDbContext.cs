using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Entities;

namespace UniversityApp.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers {  get; set; } = null!;
        public DbSet<StudentCard> StudentCards { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<StudentCourse> StudentCourses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {  
            optionsBuilder.UseSqlServer("SERVER=localhost; DATABASE=UniversityApp; Trusted_Connection=True; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(x => x.Id); 

            modelBuilder.Entity<StudentCard>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Teacher>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Course>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<StudentCourse>(entity => 
            {
                entity.HasKey(x => new { x.StudentId, x.CourseId });

                entity.HasOne(x => x.Student)
                      .WithMany(x => x.StudentCourses)
                      .HasForeignKey(x => x.StudentId);

                entity.HasOne(x => x.Course)
                      .WithMany(x => x.StudentCourses)
                      .HasForeignKey(x => x.CourseId);
            });

            modelBuilder.Entity<Course>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.TeacherId);

            modelBuilder.Entity<Student>()
                .HasOne(x => x.StudentCard)
                .WithOne(x => x.Student)
                .HasForeignKey<StudentCard>(x => x.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}