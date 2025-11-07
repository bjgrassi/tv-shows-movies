using Microsoft.EntityFrameworkCore;

using AuthService.Domain;
using AuthService.Repositories;
using AuthService.Services.Dto;
using AutoMapper;

namespace AuthService.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
    public async Task Create(AccountDto AccountDto)
    {
        // if (AccountDto.Email != null)
        //     throw new ArgumentException("User already has account.");

        // if (AccountDto.RoleID == null)
        //     AccountDto.RoleID = 3; // Default role is 'Regular'
            
        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Create(account);
    }

    public async Task Update(AccountDto AccountDto)
    {
        if (AccountDto.Email == null)
            throw new ArgumentException("User is invalid.");
        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Update(account);
    }
    public async Task Delete(AccountDto AccountDto)
    {
        if (AccountDto.Email == null)
            throw new ArgumentException("User is invalid.");
        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Delete(account);
    }
    public async Task<List<AccountDto>> GetAll()
    {
        var query = _accountRepository.GetQueryable();
        query = query.Include(account => account.Role);
        var result = query.AsEnumerable();
        if (result.Any())
            return _mapper.Map<List<AccountDto>>(result);
        return null;
    }
    public async Task<AccountDto> Get(string email)
    {
        var result = await _accountRepository.Get(email);
        return _mapper.Map<AccountDto>(result);
    }

    public async Task<AccountLoginDto> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new ArgumentException("Email and Password cannot be empty.");

        var account = await _accountRepository.GetOneByCriteria(a => a.Email == email && a.Password == password);
        if (account == null)
            throw new ArgumentException("Incorret Email or Password. Please try again...");
        return _mapper.Map<AccountLoginDto>(account);
    }
}