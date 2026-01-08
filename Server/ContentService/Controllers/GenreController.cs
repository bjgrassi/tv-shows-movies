using Microsoft.AspNetCore.Mvc;
using ContentService.Services;
using ContentService.Services.Dto;

namespace ContentService.Controllers;

[ApiController]
[Route("[controller]")] // Genre/...
public class GenreController : ControllerBase
{
    private readonly ILogger<GenreController> _logger;
    private readonly IGenreService _genreService;

    public GenreController(ILogger<GenreController> logger, IGenreService genreService)
    {
        _logger = logger;
        _genreService = genreService;
    }

    [HttpGet("GetGenres")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Retrieving all genres.");
            var result = await _genreService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all genres.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("GetGenre/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation($"Retrieving account with id = {id}.");
        try
        {
            var result = await _genreService.GetById(id);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error retrieving genre.");
            if (ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPost("CreateGenre")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenreDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] GenreDto genre)
    {
        _logger.LogInformation($"Creating a new genre with DTO: {genre.ToString()}.");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _genreService.Create(genre);
            return Created("", genre);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error creating genre.");
            if(ex.ParamName == "duplicate")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPut("UpdateGenre")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromBody] GenreDto genre)
    {
        _logger.LogInformation($"Updating genre with DTO: {genre.ToString()}.");
        try 
        {
            await _genreService.Update(genre);
            return Ok(genre);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error updating genre.");
            if (ex.ParamName == "invalid")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            if(ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpDelete("DeleteGenre")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] GenreDto genre)
    {
        _logger.LogInformation($"Deleting genre with DTO: {genre.ToString()}.");
        try 
        {
            await _genreService.Delete(genre);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error deleting genre.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }
}