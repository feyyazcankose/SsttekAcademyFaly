using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class VideoDto
{
    [SwaggerSchema("Unique identifier of the video.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Title of the video.")]
    [DefaultValue("Introduction Video")]
    public string Title { get; set; } = default!;

    // [SwaggerSchema("URL of the video.")]
    // [DefaultValue("https://example.com/video.mp4")]
    // public string Url { get; set; } = default!;

    [SwaggerSchema("Description of the video.")]
    [DefaultValue("This video introduces the course.")]
    public string Description { get; set; } = default!;

    [SwaggerSchema("Duration of the video in seconds.")]
    [DefaultValue(300)]
    public int DurationInSeconds { get; set; }
}
