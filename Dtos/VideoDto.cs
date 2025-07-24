using abaBackOffice.DTOs;

public class VideoDto : AuditableDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string>? Categories { get; set; }
    public int? Duration { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool UseExternal { get; set; }
    public bool IsPremium { get; set; }
    public IFormFile? File { get; set; } // fichier vidéo
    public IFormFile? Thumbnail { get; set; } // fichier image  
}