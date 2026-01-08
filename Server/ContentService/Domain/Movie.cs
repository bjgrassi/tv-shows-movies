namespace ContentService.Domain;

public class Movie
{
    public int MovieID { get; set; }
    public required string Title { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
    public int ReleaseYear { get; set; }
    public string? TypeName { get; set; }
    public int RunningTime { get; set; } // in minutes
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public IEnumerable<Genre>? Genres { get; set; }
}