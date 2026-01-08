namespace UserArchiveService.Domain;

using ContentService.Domain;
using AuthService.Domain;

public class UserMovieArchive
{
    public int UserMovieArchiveID { get; set; }
    public bool IsWatchLater { get; set; }
    public bool IsWatched { get; set; }
    public int MovieFK { get; set; }
    public int UserAccountFK { get; set; }
    public Movie? Movie { get; set; }
    public Account? UserAccount { get; set; }
}