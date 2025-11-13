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
    public async Task<List<AccountDtoGetRole>?> GetAll()
    {
        var query = _accountRepository.GetQueryable();
        query = query.Include(account => account.Role);
        var result = await query.ToListAsync();
        if (result.Any())
            return _mapper.Map<List<AccountDtoGetRole>>(result);
        return null;
    }
    public async Task<AccountDtoGetRole?> GetById(int accountID)
    {
        var query = _accountRepository.GetQueryable();
        query = query.Include(account => account.Role);

        var result = await query.Where(account => account.AccountID == accountID).FirstOrDefaultAsync();
        if (result != null)
            return _mapper.Map<AccountDtoGetRole>(result);
        return null;
    }
    public async Task Create(AccountDtoCreate AccountDto)
    {
        var query = _accountRepository.GetQueryable();
        query = query.Include(account => account.Role);

        var result = await query.Where(account => account.Email == AccountDto.Email).FirstOrDefaultAsync();

        if (result != null)
            throw new ArgumentException("User already has account.");

        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Create(account);
    }

    public async Task Update(AccountDtoUpdate AccountDto)
    {
        if (AccountDto.AccountID <= 0)
            throw new ArgumentException("User is invalid.");
        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Update(account);
    }
    public async Task Delete(AccountDtoUpdate AccountDto)
    {
        if (AccountDto.AccountID <= 0)
            throw new ArgumentException("User is invalid.");
        var account = _mapper.Map<Account>(AccountDto);
        await _accountRepository.Delete(account);
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