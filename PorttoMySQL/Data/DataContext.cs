using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Intern>().ToTable("Interns");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<EventType>().ToTable("EventTypes");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Organization>().ToTable("Organizations");

            modelBuilder.Entity<InternshipPoint>().ToTable("InternshipPoints");
            modelBuilder.Entity<Training>().ToTable("Trainings");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Intern> Interns { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<InternshipPoint> InternshipPoints { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}