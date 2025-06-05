using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.ToTable("JournalEntries");

            builder.HasKey(e => e.JournalEntryId);

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(250);

            // One-to-many: JournalEntry → JournalEntryLine
            builder.HasMany(e => e.Lines)
                   .WithOne() 
                   .HasForeignKey(l => l.JournalEntryId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
