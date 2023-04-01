using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StudentManagementApp.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
