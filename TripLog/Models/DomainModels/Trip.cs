using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripLog.Models
{
    public class Trip
    {
        public int TripId { get; set; }                     // PK

        [Range(1, 9999999, ErrorMessage = "Please select a destination.")]
        public int DestinationId { get; set; }              // FK 
        public Destination Destination { get; set; }        // navigation property

        [Required(ErrorMessage = "Please enter the date your trip starts.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please enter the date your trip ends.")]
        public DateTime? EndDate { get; set; }

        public int? AccommodationId { get; set; }           // FK (nullable)
        public Accommodation Accommodation { get; set; }    // navigation property

        // navigation property to linking entity for many-to-many with Activity
        public ICollection<TripActivity> TripActivities { get; set; }
    }
}
