﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripLog.Models
{
    internal class NoteConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> entity)
        {
            // configure foreign keys so don't use cascading delete
            entity.HasOne(t => t.Destination)
                .WithMany(d => d.Note)
                .OnDelete(DeleteBehavior.Restrict);

            // accommodation can be null 
            entity.HasOne(t => t.Accommodation)
                .WithMany(a => a.Notes)
                .OnDelete(DeleteBehavior.SetNull);  
        }
    }

}