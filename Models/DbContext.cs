using MyExam.Models;
using Microsoft.EntityFrameworkCore;

namespace MyExam.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgreementModel>().ToTable("Agreement");
        }
        public DbSet<AgreementModel> Agreements { get; set; }
    }
}
