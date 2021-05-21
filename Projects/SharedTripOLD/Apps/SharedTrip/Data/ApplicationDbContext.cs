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

        public DbSet<User> Users  { get; set; }

        public DbSet<Trip> Trips  { get; set; }

       public DbSet<UserTrip> UsersTrips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SharedTrip;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTrip>().HasKey(x => new { x.TripId, x.UserId });

            modelBuilder.Entity<UserTrip>()
               .HasOne(t => t.User)
               .WithMany(s => s.UserTrips)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserTrip>()
              .HasOne(t => t.Trip)
              .WithMany(s => s.UserTrips)
              .HasForeignKey(x => x.TripId);

        }
    }
}
