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
    public class UserUseCaseConfiguration : IEntityTypeConfiguration<UserUseCase>
    {
        public void Configure(EntityTypeBuilder<UserUseCase> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.UseCaseId });

            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserUseCases)
                   .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.UseCase)
                   .WithMany(uc => uc.UserUseCases)
                   .HasForeignKey(uc => uc.UseCaseId);
        }
    }
}
