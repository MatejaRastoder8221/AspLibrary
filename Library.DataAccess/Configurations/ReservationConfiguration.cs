using Library.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.Status)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Book)
                   .WithMany(b => b.Reservations)
                   .HasForeignKey(r => r.BookId);
        }
    }
}
