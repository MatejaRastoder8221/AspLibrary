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
    internal class BorrowRecordConfiguration : EntityConfiguration<BorrowRecord>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<BorrowRecord> builder)
        {
            builder.HasOne(br => br.User)
                   .WithMany(u => u.BorrowRecords)
                   .HasForeignKey(br => br.UserId);
            builder.HasOne(br => br.Book)
                   .WithMany(b => b.BorrowRecords)
                   .HasForeignKey(br => br.BookId);
        }
    }
}
