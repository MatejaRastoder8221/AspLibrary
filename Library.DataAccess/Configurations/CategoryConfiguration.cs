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
    internal class CategoryConfiguration : NamedEntityConfiguration<Category>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Description)
                   .HasMaxLength(200);
        }
    }
}
