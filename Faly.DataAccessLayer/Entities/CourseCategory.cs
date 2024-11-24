namespace Faly.DataAccessLayer.Entities;

public class CourseCategory
{
    public int CourseId { get; set; }
    public int CategoryId { get; set; }

    // Navigation Properties
    public Course Course { get; set; }
    public Category Category { get; set; }
}
