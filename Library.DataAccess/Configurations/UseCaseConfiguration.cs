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
    public class UseCaseConfiguration : IEntityTypeConfiguration<UseCase>
    {
        public void Configure(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(uc => uc.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
