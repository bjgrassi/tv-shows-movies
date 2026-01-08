namespace ContentService.Domain;

public class Serie
{
    public int SerieID { get; set; }
    public required string Title { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
    public string? TypeName { get; set; }
    public int NumOfSeasons { get; set; }
    public bool IsFinished { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}