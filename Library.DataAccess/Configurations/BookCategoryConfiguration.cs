using Library.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });
            builder.HasOne(bc => bc.Book)
                   .WithMany(b => b.BookCategories)
                   .HasForeignKey(bc => bc.BookId);
            builder.HasOne(bc => bc.Category)
                   .WithMany(c => c.BookCategories)
                   .HasForeignKey(bc => bc.CategoryId);

        }
    }
}
