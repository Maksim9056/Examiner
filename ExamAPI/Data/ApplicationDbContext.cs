using ExamModels;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExamAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
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
    }
}
