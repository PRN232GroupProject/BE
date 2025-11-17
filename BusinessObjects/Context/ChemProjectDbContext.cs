using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;

namespace BusinessObjects.Context
{
    public class ChemProjectDbContext : DbContext
    {
        public ChemProjectDbContext()
        {
        }

        public ChemProjectDbContext(DbContextOptions<ChemProjectDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<StudentTestSession> StudentTestSessions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ChemistryPrepV1");
            base.OnModelCreating(modelBuilder);

            // Role - User relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chapter - Lesson relationship
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Chapter)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.CreatedBy)
                .WithMany()
                .HasForeignKey(l => l.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);

            // Resource - Lesson relationship
            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Lesson)
                .WithMany(l => l.Resources)
                .HasForeignKey(r => r.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question - Lesson relationship
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Questions)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Question - CreatedBy relationship
            modelBuilder.Entity<Question>()
                .HasOne(q => q.CreatedBy)
                .WithMany(u => u.CreatedQuestions)
                .HasForeignKey(q => q.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TestQuestion - Composite Primary Key
            modelBuilder.Entity<TestQuestion>()
                .HasKey(tq => new { tq.TestId, tq.QuestionId });

            modelBuilder.Entity<TestQuestion>()
                .HasOne(tq => tq.Test)
                .WithMany(t => t.TestQuestions)
                .HasForeignKey(tq => tq.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestQuestion>()
                .HasOne(tq => tq.Question)
                .WithMany(q => q.TestQuestions)
                .HasForeignKey(tq => tq.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Test - CreatedBy relationship
            modelBuilder.Entity<Test>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTests)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentTestSession - User relationship
            modelBuilder.Entity<StudentTestSession>()
                .HasOne(sts => sts.User)
                .WithMany(u => u.StudentTestSessions)
                .HasForeignKey(sts => sts.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentTestSession - Test relationship
            modelBuilder.Entity<StudentTestSession>()
                .HasOne(sts => sts.Test)
                .WithMany(t => t.StudentTestSessions)
                .HasForeignKey(sts => sts.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentAnswer - Session relationship
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Session)
                .WithMany(sts => sts.StudentAnswers)
                .HasForeignKey(sa => sa.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentAnswer - Question relationship
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Question)
                .WithMany(q => q.StudentAnswers)
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}