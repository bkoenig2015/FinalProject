﻿using System.Collections.Generic;

namespace TripLog.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }           // PK 
        public string Name { get; set; }

        public ICollection<Note> Note { get; set; }     // navigation property
    }
}
