using Microsoft.AspNetCore.Mvc;

using AuthService.Services;
using AuthService.Services.Dto;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")] // Auth/...
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAccountService _accountService;
    private readonly IRoleService _roleService;

    public AuthController(ILogger<AuthController> logger, IAccountService accountService, IRoleService roleService)
    {
        _logger = logger;
        _accountService = accountService;
        _roleService = roleService;
    }

    [HttpGet("GetAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccountDtoGetRole>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Retrieving all users.");
            var result = await _accountService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("GetAccount/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoGetRole))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation($"Retrieving account with id = {id}.");
        try
        {
            var result = await _accountService.GetById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving account.");
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status404NotFound, ex.Message));
        }
    }

    [HttpPost("CreateAccount")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccountDtoCreate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Create([FromBody] AccountDtoCreate account)
    {
        _logger.LogInformation($"Creating a new account with DTO: {account.ToString()}.");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _accountService.Create(account);
            return Created("", account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating account.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }

    [HttpPut("UpdateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoUpdate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromBody] AccountDtoUpdate account)
    {
        _logger.LogInformation($"Updating user with DTO: {account.ToString()}.");
        try 
        {
            await _accountService.Update(account);
            return Ok(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating account.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }

    [HttpDelete("DeleteAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoUpdate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] AccountDtoUpdate account)
    {
        _logger.LogInformation($"Deleting account with DTO: {account.ToString()}.");
        try 
        {
            await _accountService.Delete(account);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting account.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }

    [HttpGet("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Login(string email, string password)
    {
        _logger.LogInformation($"logging in user: login/email:{email} - password: {password}");
        try
        {
            var result = await _accountService.Login(email, password);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login.");
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message));
        }
    }

    [HttpGet("GetRoles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDto>))]
    public async Task<IActionResult> GetAllRoles()
    {
        _logger.LogInformation("Retrieving all roles.");
        try
        {
            var result = await _roleService.GetAll();
            if (result == null || result.Count == 0)
                return Ok(new List<RoleDto>()); // Return empty list
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all roles.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}