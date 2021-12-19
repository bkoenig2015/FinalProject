using Microsoft.EntityFrameworkCore;

namespace TripLog.Models
{
    public class NoteLogContext : DbContext
    {
        public NoteLogContext(DbContextOptions<NoteLogContext> options)
            : base(options)
        { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfig());
        }

    }
}
