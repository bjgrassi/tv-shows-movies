using Microsoft.AspNetCore.Mvc;
using ContentService.Services;
using ContentService.Services.Dto;

namespace ContentService.Controllers;

[ApiController]
[Route("[controller]")] // Serie/...
public class SerieController : ControllerBase
{
    private readonly ILogger<SerieController> _logger;
    private readonly ISerieService _serieService;

    public SerieController(ILogger<SerieController> logger, ISerieService serieService)
    {
        _logger = logger;
        _serieService = serieService;
    }

    [HttpGet("GetSeries")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SerieDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Retrieving all series.");
            var result = await _serieService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all series.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("GetSerie/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SerieDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation($"Retrieving account with id = {id}.");
        try
        {
            var result = await _serieService.GetById(id);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error retrieving serie.");
            if (ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPost("CreateSerie")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SerieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] SerieDto serie)
    {
        _logger.LogInformation($"Creating a new serie with DTO: {serie.ToString()}.");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _serieService.Create(serie);
            return Created("", serie);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error creating serie.");
            if(ex.ParamName == "duplicate")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpPut("UpdateSerie")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SerieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromBody] SerieDto serie)
    {
        _logger.LogInformation($"Updating serie with DTO: {serie.ToString()}.");
        try 
        {
            await _serieService.Update(serie);
            return Ok(serie);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error updating serie.");
            if (ex.ParamName == "invalid")
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
            if(ex.ParamName == "notfound")
                return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpDelete("DeleteSerie")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SerieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] SerieDto serie)
    {
        _logger.LogInformation($"Deleting serie with DTO: {serie.ToString()}.");
        try 
        {
            await _serieService.Delete(serie);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error deleting serie.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }
}