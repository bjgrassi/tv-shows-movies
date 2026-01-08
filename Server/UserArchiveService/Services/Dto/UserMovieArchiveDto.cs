using ContentService.Services.Dto;

namespace UserArchiveService.Services.Dto;

public class UserMovieArchiveDto
{
    public int UserMovieArchiveID { get; set; }
    public bool IsWatchLater { get; set; }
    public bool IsWatched { get; set; }
    public int MovieFK { get; set; }
    public int UserAccountFK { get; set; }
    public MovieDto? Movie { get; set; }
}