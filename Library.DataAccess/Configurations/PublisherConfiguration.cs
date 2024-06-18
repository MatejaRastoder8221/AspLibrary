using Library.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal class PublisherConfiguration : NamedEntityConfiguration<Publisher>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(x => x.Address)
                   .HasMaxLength(200);
        }
    }
}
