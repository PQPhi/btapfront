using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Class entity
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateOfBirth).IsRequired();

                // Configure foreign key relationship
                entity.HasOne(s => s.Class)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Lớp 10A1", Description = "Lớp 10 chuyên Toán" },
                new Class { Id = 2, Name = "Lớp 10A2", Description = "Lớp 10 chuyên Lý" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "Nguyễn Văn A", Email = "nva@example.com", DateOfBirth = new DateTime(2008, 5, 15), ClassId = 1 },
                new Student { Id = 2, FullName = "Trần Thị B", Email = "ttb@example.com", DateOfBirth = new DateTime(2008, 8, 20), ClassId = 1 },
                new Student { Id = 3, FullName = "Lê Văn C", Email = "lvc@example.com", DateOfBirth = new DateTime(2008, 3, 10), ClassId = 2 }
            );
        }
    }
}
