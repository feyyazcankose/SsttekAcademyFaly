using Faly.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faly.DataAccessLayer.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<CourseCategory> CourseCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSection> CourseSections { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CourseSection ve Course arasındaki ilişki
        modelBuilder
            .Entity<CourseSection>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.Sections)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Video ve CourseSection arasındaki ilişki
        modelBuilder
            .Entity<Video>()
            .HasOne(v => v.CourseSection)
            .WithMany(cs => cs.Videos)
            .HasForeignKey(v => v.CourseSectionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order ve ApplicationUser (User) arasındaki ilişki
        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order ve Payment arasındaki bire bir ilişki
        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderDetail için bileşik anahtar
        modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.CourseId });

        // OrderDetail ve Order arasındaki ilişki
        modelBuilder
            .Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        // OrderDetail ve Course arasındaki ilişki
        modelBuilder
            .Entity<OrderDetail>()
            .HasOne(od => od.Course)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(od => od.CourseId);

        // CourseCategory için bileşik anahtar
        modelBuilder.Entity<CourseCategory>().HasKey(cc => new { cc.CourseId, cc.CategoryId });

        // CourseCategory ve Course arasındaki ilişki
        modelBuilder
            .Entity<CourseCategory>()
            .HasOne(cc => cc.Course)
            .WithMany(c => c.CourseCategories)
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // CourseCategory ve Category arasındaki ilişki
        modelBuilder
            .Entity<CourseCategory>()
            .HasOne(cc => cc.Category)
            .WithMany(cat => cat.CourseCategories)
            .HasForeignKey(cc => cc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
