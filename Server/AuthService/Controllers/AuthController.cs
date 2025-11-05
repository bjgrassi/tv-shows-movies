using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

using AuthService.Services;
using AuthService.Services.Dto;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost("CreateAccount")]
    public async Task<IActionResult> Create([FromBody] AccountDto user)
    {
        _logger.LogInformation($"Saving new user with DTO: {user.ToString()}.");
        await _accountService.Create(user);
        return Ok();
    }

    [HttpPost("UpdateAccount")]
    public async Task<IActionResult> Update([FromBody] AccountDto user)
    {
        _logger.LogInformation($"Updating user with DTO: {user.ToString()}.");
        await _accountService.Update(user);
        return Ok();
    }

    [HttpPost("DeleteAccount")]
    public async Task<IActionResult> Delete([FromBody] AccountDto user)
    {
        _logger.LogInformation($"Deleting user with DTO: {user.ToString()}.");
        await _accountService.Delete(user);
        return Ok();
    }

    [HttpGet("GetAccounts")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Retrieving all users.");
        var result = await _accountService.GetAll();
        if (result == null || result.Count == 0)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("GetAccount/{email}")]
    public async Task<IActionResult> GetById([FromQuery] string email)
    {
        _logger.LogInformation($"Retrieving user with email = {email}.");
        var result = await _accountService.Get(email);
        if (result == null)
            return NotFound();
        return Ok(result);
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