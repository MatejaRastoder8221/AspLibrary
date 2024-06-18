using Library.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal class ReviewConfiguration : EntityConfiguration<Review>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Book)
                   .WithMany(b => b.Reviews)
                   .HasForeignKey(r => r.BookId);
        }
    }
}
