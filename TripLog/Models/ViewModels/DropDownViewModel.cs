using System.Collections.Generic;

namespace TripLog.Models
{
    public class DropDownViewModel
    {
        public IEnumerable<Destination> Destinations { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }
    }
}
