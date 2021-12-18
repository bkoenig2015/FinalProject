using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripLog.Models
{
    internal class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> entity)
        {
            // configure foreign keys so don't use cascading delete
            entity.HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .OnDelete(DeleteBehavior.Restrict);

            // accommodation can be null 
            entity.HasOne(t => t.Accommodation)
                .WithMany(a => a.Trips)
                .OnDelete(DeleteBehavior.SetNull);  
        }
    }

}
