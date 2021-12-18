namespace TripLog.Models
{
    public class TripActivity
    {
        // composite primary key
        public int TripId { get; set; }         // FK 
        public int ActivityId { get; set; }     // FK 

        // navigation properties
        public Trip Trip { get; set; }
        public Activity Activity { get; set; }
    }
}
