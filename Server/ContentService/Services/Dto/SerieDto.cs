namespace ContentService.Services.Dto;

public class SerieDto
{
    public int SerieID { get; set; }
    public required string Title { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
    public string? TypeName { get; set; }
    public int NumOfSeasons { get; set; }
    public bool IsFinished { get; set; }
}