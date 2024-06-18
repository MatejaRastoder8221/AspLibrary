using Library.domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Configurations
{
    internal abstract class NamedEntityConfiguration<T> : EntityConfiguration<T>
        where T : NamedEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.HasIndex(x => x.Name)
                   .IsUnique();
        }
    }
}
