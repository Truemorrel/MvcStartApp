using Microsoft.EntityFrameworkCore;
//using MvcStartApp.Models.LogRepo;

namespace MvcStartApp.Models.LogRepo
{
    public sealed class LogContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>()
                .ToTable("Requests")
                .HasKey(r => r.Id);
        }
    }
}
