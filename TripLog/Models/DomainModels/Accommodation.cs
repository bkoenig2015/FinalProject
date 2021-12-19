using System.Collections.Generic;

namespace TripLog.Models
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }              // PK 
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Note> Notes { get; set; }          // navigation property
    }
}
