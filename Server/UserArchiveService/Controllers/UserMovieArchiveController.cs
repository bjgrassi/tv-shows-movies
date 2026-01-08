using Microsoft.AspNetCore.Mvc;
using UserArchiveService.Services;
using UserArchiveService.Services.Dto;

namespace UserArchiveService.Controllers;

[ApiController]
[Route("[controller]")] // UserMovieArchive/...
public class UserMovieArchiveController : ControllerBase
{
    private readonly ILogger<UserMovieArchiveController> _logger;
    private readonly IUserMovieArchiveService _userMovieArchiveService;

    public UserMovieArchiveController(ILogger<UserMovieArchiveController> logger, IUserMovieArchiveService userMovieArchiveService)
    {
        _logger = logger;
        _userMovieArchiveService = userMovieArchiveService;
    }

    [HttpGet("GetAllUserMovies/{userID}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMovieArchiveDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUserMovies([FromRoute] int userID)
    {
        try
        {
            _logger.LogInformation("Retrieving all user movies.");
            var result = await _userMovieArchiveService.GetAllUserMovies(userID);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all user movies.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}