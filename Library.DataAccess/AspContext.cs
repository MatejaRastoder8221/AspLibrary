using Library.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Library.DataAccess
{
    public class AspContext : DbContext
    {
        private readonly string _connectionString;

        // Constructor for dependency injection
        public AspContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Parameterless constructor for quick testing (optional)
        public AspContext()
        {
            _connectionString = "Data Source=MARABUNTA-SUPER;Initial Catalog=LibraryProject;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Manage the created and updated timestamps for entities
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }

        // DbSets for each entity
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<Image> Images { get; set; } // Renamed from File to Image
        public DbSet<ErrorLog> ErrorLogs { get; set; } // Added ErrorLogs DbSet
    }
}
