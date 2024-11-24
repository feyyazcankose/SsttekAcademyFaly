namespace Faly.DataAccessLayer.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } // Kategori adı
    public string Description { get; set; } // Kategori açıklaması (opsiyonel)
    public bool IsActive { get; set; } // Kategori aktif mi?

    // Navigation Properties
    public ICollection<CourseCategory> CourseCategories { get; set; }
}
