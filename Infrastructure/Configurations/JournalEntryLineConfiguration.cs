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
    public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
    {
        public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
        {
            builder.ToTable("JournalEntryLines");

            builder.HasKey(l => l.JournalEntryLineId);

            builder.Property(l => l.Debit)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(l => l.Credit)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

           
        }
    }
}
