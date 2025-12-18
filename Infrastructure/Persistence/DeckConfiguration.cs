using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DeckConfiguration : IEntityTypeConfiguration<Deck>
    {
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.ToTable("Decks");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title)
                .IsRequired()               
                .HasMaxLength(100)          
                .HasColumnType("varchar(100)"); 

            builder.Property(d => d.Description)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.HasMany(d => d.Cards)
                .WithOne(c => c.Deck)
                .HasForeignKey(c => c.DeckId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
