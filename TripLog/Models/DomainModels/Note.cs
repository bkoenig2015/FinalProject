
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripLog.Models
{
    public class Note
    {
        public int NoteId { get; set; }                     // PK

        [Range(1, 9999999, ErrorMessage = "Please select a destination.")]
        public int CategoryId { get; set; }              // FK 
        public Category Category { get; set; }        // navigation property


        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Please enter the date your note needs to be done with.")]
        public DateTime? DueDate { get; set; }

        public string Title { get; set; }    

        // navigation property to linking entity for many-to-many with Activity
    }
}
