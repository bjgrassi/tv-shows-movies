namespace ContentService.Domain;

public class Genre
{
    public int GenreID { get; set; }
    public required string Name { get; set; }
    public IEnumerable<Movie>? Movies { get; set; }
}