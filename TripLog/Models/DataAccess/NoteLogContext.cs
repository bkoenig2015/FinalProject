using Microsoft.EntityFrameworkCore;

namespace TripLog.Models
{
    public class NoteLogContext : DbContext
    {
        public NoteLogContext(DbContextOptions<NoteLogContext> options)
            : base(options)
        { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfig());
        }

    }
}
