using System.Collections.Generic;

namespace TripLog.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }      // PK
        public string Name { get; set; }

        // navigation property to linking entity for many-to-many with Trip
        public ICollection<TripActivity> TripActivities { get; set; }
    }
}
