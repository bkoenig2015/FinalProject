using System.Collections.Generic;

namespace TripLog.Models
{
    public class Category
    {
        public int CategoryId { get; set; }           // PK 
        public string Name { get; set; }

        public ICollection<Note> Note { get; set; }     // navigation property
    }
}
