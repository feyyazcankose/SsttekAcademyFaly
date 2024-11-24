using Faly.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faly.DataAccessLayer.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<CourseCategory> CourseCategory { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSection> CourseSection { get; set; }
    public DbSet<Video> Video { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<CourseSection>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.Sections)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Bir kurs silindiğinde bölümleri de silinsin.

        modelBuilder
            .Entity<Video>()
            .HasOne(v => v.CourseSection)
            .WithMany(cs => cs.Videos)
            .HasForeignKey(v => v.CourseSectionId)
            .OnDelete(DeleteBehavior.Cascade); // Bir bölüm silindiğinde videolar da silinsin.

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde siparişleri de silinsin.

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId);

        modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.CourseId }); // Composite Key (Bir siparişin bir kursa özel detayı)

        modelBuilder
            .Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        modelBuilder
            .Entity<OrderDetail>()
            .HasOne(od => od.Course)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(od => od.CourseId);

        modelBuilder
            .Entity<Payment>()
            .HasOne(p => p.Order)
            .WithOne()
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // Sipariş silindiğinde ödeme bilgileri de silinsin.

        modelBuilder.Entity<CourseCategory>().HasKey(cc => new { cc.CourseId, cc.CategoryId }); // Composite Key

        modelBuilder
            .Entity<CourseCategory>()
            .HasOne(cc => cc.Course)
            .WithMany(c => c.CourseCategories)
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Bir kurs silindiğinde bu ilişki de silinsin.

        modelBuilder
            .Entity<CourseCategory>()
            .HasOne(cc => cc.Category)
            .WithMany(cat => cat.CourseCategories)
            .HasForeignKey(cc => cc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Bir kategori silindiğinde bu ilişki de silinsin.
    }
}
