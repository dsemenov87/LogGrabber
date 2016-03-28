using Microsoft.Data.Entity;

namespace LogGrabber.DAL
{
    public class LogGrabberDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .ToTable("Application")
                .Property(e => e.Name).IsRequired();

            modelBuilder.Entity<Error>()
                .ToTable("Error")
                .Property(e => e.Message).IsRequired();

            modelBuilder.Entity<Error>()
                .Property(e => e.Name).IsRequired();

            modelBuilder.Entity<Error>()
                .Property(e => e.CallStack).IsRequired();

            modelBuilder.Entity<LogItem>()
                .ToTable("LogItem")
                .Property(e => e.Message).IsRequired();

            modelBuilder.Entity<LogItem>()
                .Property(e => e.Status).IsRequired();

            modelBuilder.Entity<LogItem>()
                .Property(e => e.Occured).IsRequired();

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32);

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32);
        }

        public DbSet<LogItem> Logs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

