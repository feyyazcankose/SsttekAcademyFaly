namespace Faly.DataAccessLayer.Entities;

public class CourseSection
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; } // Bölüm başlığı
    public string Description { get; set; } // Bölüm açıklaması

    // Navigation Properties
    public Course Course { get; set; }
    public ICollection<Video> Videos { get; set; }
}
