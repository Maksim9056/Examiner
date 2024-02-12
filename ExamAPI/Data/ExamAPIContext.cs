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
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<User_roles> User_Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Questions> Questions { get; set; } = null!;
        public DbSet<TestQuestion> TestQuestion { get; set; } = null!;
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; } = null!;
        public DbSet<ExamsTest> ExamsTest { get; set; } = null!;
        public DbSet<UserExams> UserExams { get; set; } = null!;
        public DbSet<Answer> Answer { get; set; } = null!;
        public DbSet<Options> Options { get; set; } = null!;
        public DbSet<Test> Test { get; set; } = null!;
        public DbSet<Exam> Exam { get; set; } = null!;
        public DbSet<Exams> Exams { get; set; } = null!;
        public DbSet<Save_results> Save_Results { get; set; } = null!;
        public DbSet<Filles> Filles { get; set; } = null!;

        //public DbSet<ExamModels.Answer> Answer { get; set; } = default!;

        //public DbSet<ExamModels.User> Users { get; set; } = default!;
        //public DbSet<ExamModels.User_roles> User_roles { get; set; } = default!;
        //public DbSet<ExamModels.Roles> Roles { get; set; } = default!;
        //public DbSet<ExamModels.Questions> Questions { get; set; } = default!;
        //public DbSet<ExamModels.TestQuestion> TestQuestion { get; set; } = default!;
        //public DbSet<ExamModels.QuestionAnswer> QuestionAnswer { get; set; } = default!;
        //public DbSet<ExamModels.ExamsTest> ExamsTest { get; set; } = default!;
        //public DbSet<ExamModels.UserExams> UserExams { get; set; } = default!;
        //public DbSet<ExamModels.Options> Options { get; set; } = default!;
        //public DbSet<ExamModels.Test> Test { get; set; } = default!;
        //public DbSet<ExamModels.Exams> Exams { get; set; } = default!;
        //public DbSet<ExamModels.Save_results> Save_results { get; set; } = default!;
        //public DbSet<ExamModels.Filles> Filles { get; set; } = default!;
        //public DbSet<ExamModels.Exam> Exam { get; set; } = default!;
    }
}
