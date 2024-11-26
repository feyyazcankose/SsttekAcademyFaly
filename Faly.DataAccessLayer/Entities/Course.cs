namespace Faly.DataAccessLayer.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } // Kurs adı
    public string Description { get; set; } // Kurs açıklaması

    public string? CoverImage { get; set; } // Kurs açıklaması
    public decimal Price { get; set; } // Kurs fiyatı
    public bool IsActive { get; set; } // Kurs aktif mi?

    // Navigation Properties
    public ICollection<CourseCategory> CourseCategories { get; set; }
    public ICollection<CourseSection> Sections { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
