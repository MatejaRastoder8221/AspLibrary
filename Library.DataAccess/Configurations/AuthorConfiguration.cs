using Library.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    internal class AuthorConfiguration : EntityConfiguration<Author>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.Biography)
                   .HasMaxLength(1000);
        }
    }
}
