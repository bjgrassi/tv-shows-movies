using System.Text.Json;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromQuery] int accountID)
    {
        _logger.LogInformation($"Retrieving account with id = {accountID}.");
        var result = await _accountService.GetById(accountID);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("CreateAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] AccountDto account)
    {
        _logger.LogInformation($"Saving new account with DTO: {account.ToString()}.");
        await _accountService.Create(account);
        return CreatedAtAction(nameof(GetById), new { accountID = account.AccountID }, account);
    }

    [HttpPut("UpdateAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] AccountDto account)
    {
        _logger.LogInformation($"Updating user with DTO: {account.ToString()}.");
        if (account.AccountID <= 0)
        {
            return NotFound();
        }

        var accountItem = await _accountService.GetById(account.AccountID);

        if (accountItem?.AccountID != account.AccountID)
        {
            return BadRequest();
        }

        await _accountService.Update(account);
        return NoContent();
    }

    [HttpDelete("DeleteAccount")]
    public async Task<IActionResult> Delete([FromBody] AccountDto account)
    {
        _logger.LogInformation($"Deleting account with DTO: {account.ToString()}.");
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