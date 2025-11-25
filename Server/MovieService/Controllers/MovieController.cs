using Microsoft.AspNetCore.Mvc;
using MovieService.Services;
using MovieService.Services.Dto;

namespace MovieController.Controllers;

[ApiController]
[Route("[controller]")] // Movie/...
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(ILogger<MovieController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    [HttpGet("GetMovies")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Retrieving all movies.");
            var result = await _movieService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all movies.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("GetMovie/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation($"Retrieving account with id = {id}.");
        try
        {
            var result = await _movieService.GetById(id);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error retrieving movie.");
            if (ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPost("CreateMovie")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] MovieDto movie)
    {
        _logger.LogInformation($"Creating a new movie with DTO: {movie.ToString()}.");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _movieService.Create(movie);
            return Created("", movie);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error creating movie.");
            if(ex.ParamName == "duplicate")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPut("UpdateMovie")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromBody] MovieDto movie)
    {
        _logger.LogInformation($"Updating movie with DTO: {movie.ToString()}.");
        try 
        {
            await _movieService.Update(movie);
            return Ok(movie);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error updating movie.");
            if (ex.ParamName == "invalid")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            if(ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpDelete("DeleteMovie")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] MovieDto movie)
    {
        _logger.LogInformation($"Deleting movie with DTO: {movie.ToString()}.");
        try 
        {
            await _movieService.Delete(movie);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error deleting movie.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }
}