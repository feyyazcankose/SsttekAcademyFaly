namespace Faly.Models;

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Categories { get; set; }
    public List<CourseSectionViewModel> Sections { get; set; }
}

public class CourseSectionViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<VideoViewModel> Videos { get; set; }
}

public class VideoViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
}
