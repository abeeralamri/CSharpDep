using Microsoft.EntityFrameworkCore;
namespace Exam.Models
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions options) : base(options) {}
        
            public DbSet<User> Users {get; set;}
            public DbSet<Hobby> Hobbies {get; set;}
            public DbSet<UserHobby> UserHobbies {get;set;}

 
        
    }
}