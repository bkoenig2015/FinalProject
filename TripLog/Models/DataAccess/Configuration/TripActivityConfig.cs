using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripLog.Models
{
    internal class TripActivityConfig : IEntityTypeConfiguration<TripActivity>
    {
        public void Configure(EntityTypeBuilder<TripActivity> entity)
        {
            // composite primary key for BookAuthor
            entity.HasKey(ta => new { ta.TripId, ta.ActivityId });

            // one-to-many relationship between Trip and TripActivity
            entity.HasOne(ta => ta.Trip)
                .WithMany(t => t.TripActivities)
                .HasForeignKey(ta => ta.TripId);

            // one-to-many relationship between Activity and TripActivity
            entity.HasOne(ta => ta.Activity)
                .WithMany(a => a.TripActivities)
                .HasForeignKey(ta => ta.ActivityId);
        }
    }
}
