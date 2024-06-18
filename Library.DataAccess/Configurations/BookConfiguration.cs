using Library.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal class BookConfiguration : EntityConfiguration<Book>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.ISBN)
                   .IsRequired()
                   .HasMaxLength(13);
            builder.HasOne(x => x.Publisher)
                   .WithMany(p => p.Books)
                   .HasForeignKey(x => x.PublisherId);
        }
    }
}
