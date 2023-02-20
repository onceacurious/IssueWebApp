using IssueWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueWebApp.Data
{
   public class ApplicationDbContext : DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }

      public DbSet<Division> Divisions { get; set; }
      public DbSet<Issue> Issues { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<UserBio> Bio { get; set; }
      public DbSet<RefreshToken> RefreshTokens { get; set; }
      public DbSet<Answer> Answers { get; set; }
      public DbSet<Comment> Comments { get; set; }
   }
}