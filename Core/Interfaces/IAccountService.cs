
using Core.Models.Account;

namespace Core.Interfaces;

public interface IAccountService
{
    public Task<string> Register(RegisterModel model);
}
