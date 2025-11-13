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
        _logger.LogInformation("Retrieving all users.");
        var result = await _accountService.GetAll();
        if (result == null || result.Count == 0)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("GetAccount/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoGetRole))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation($"Retrieving account with id = {id}.");
        var result = await _accountService.GetById(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("CreateAccount")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccountDtoCreate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] AccountDtoCreate account)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _logger.LogInformation($"Saving new account with DTO: {account.ToString()}.");
        await _accountService.Create(account);
        return Created("", account);
    }

    [HttpPut("UpdateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoUpdate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Update([FromBody] AccountDtoUpdate account)
    {
        _logger.LogInformation($"Updating user with DTO: {account.ToString()}.");
        if (account.AccountID <= 0)
        {
            return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, "Invalid id."));
        }

        var accountItem = await _accountService.GetById(account.AccountID);

        if (accountItem?.AccountID != account.AccountID)
        {
            return NotFound();
        }

        await _accountService.Update(account);
        return Ok();
    }

    [HttpDelete("DeleteAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDtoUpdate))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete([FromBody] AccountDtoUpdate account)
    {
        _logger.LogInformation($"Deleting account with DTO: {account.ToString()}.");
        if (account.AccountID <= 0)
        {
            return BadRequest();
        }

        var accountItem = await _accountService.GetById(account.AccountID);

        if (accountItem?.AccountID != account.AccountID)
        {
            return NotFound();
        }
        
        await _accountService.Delete(account);
        return Ok();
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        _logger.LogInformation($"logging in user: login/email:{email} - password: {password}");
        var result = await _accountService.Login(email, password);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        _logger.LogInformation("Retrieving all roles.");
        var result = await _roleService.GetAll();
        if (result == null || result.Count == 0)
            return NotFound();
        return Ok(result);
    }
}