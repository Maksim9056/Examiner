using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamModels;

namespace ExamAPI.Data
{
    public class ExamAPIContext : DbContext
    {
        public ExamAPIContext (DbContextOptions<ExamAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ExamModels.Answer> Answer { get; set; } = default!;

        public DbSet<ExamModels.User> User { get; set; } = default!;
        public DbSet<ExamModels.User_roles> User_roles { get; set; } = default!;
        public DbSet<ExamModels.Roles> Roles { get; set; } = default!;
        public DbSet<ExamModels.Questions> Questions { get; set; } = default!;
        public DbSet<ExamModels.TestQuestion> TestQuestion { get; set; } = default!;
        public DbSet<ExamModels.QuestionAnswer> QuestionAnswer { get; set; } = default!;
        public DbSet<ExamModels.ExamsTest> ExamsTest { get; set; } = default!;
        public DbSet<ExamModels.UserExams> UserExams { get; set; } = default!;
        public DbSet<ExamModels.Options> Options { get; set; } = default!;
        public DbSet<ExamModels.Test> Test { get; set; } = default!;
        public DbSet<ExamModels.Exams> Exams { get; set; } = default!;
        public DbSet<ExamModels.Save_results> Save_results { get; set; } = default!;
        public DbSet<ExamModels.Filles> Filles { get; set; } = default!;
        public DbSet<ExamModels.Exam> Exam { get; set; } = default!;
    }
}
