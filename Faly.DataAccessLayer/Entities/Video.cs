namespace Faly.DataAccessLayer.Entities;

public class Video
{
    public int Id { get; set; }
    public int CourseSectionId { get; set; }
    public string Title { get; set; } // Video başlığı
    public string Url { get; set; } // Video URL'si (YouTube, Vimeo veya dosya yolu)
    public string Description { get; set; } // Video açıklaması
    public int DurationInSeconds { get; set; } // Video süresi

    // Navigation Properties
    public CourseSection CourseSection { get; set; }
}
