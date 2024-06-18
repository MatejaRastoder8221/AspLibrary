using Library.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.BirthDate)
                .IsRequired();

            builder.HasOne(u => u.Image)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.BorrowRecords)
                .WithOne(br => br.User)
                .HasForeignKey(br => br.UserId);

            builder.HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            builder.HasMany(u => u.UserUseCases)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            // Additional configurations if necessary
        }
    }
}
