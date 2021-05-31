namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UsersTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SharedTrip;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UserTrip>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserTrips)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<UserTrip>()
               .HasOne(u => u.Trip)
               .WithMany(u => u.TripUsers)
               .HasForeignKey(u => u.TripId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTrip>().HasKey(x => new { x.UserId, x.TripId });
        }
    }
}
