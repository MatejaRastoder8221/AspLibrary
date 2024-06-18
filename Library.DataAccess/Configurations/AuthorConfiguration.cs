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
    internal class AuthorConfiguration : EntityConfiguration<Author>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.Property(x => x.Biography)
                   .HasMaxLength(1000);
        }
    }
}
