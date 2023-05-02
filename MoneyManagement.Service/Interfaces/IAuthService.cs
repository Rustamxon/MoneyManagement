
using MoneyManagement.Service.DTOs.Login;

namespace MoneyManagement.Service.Interfaces;

public interface IAuthService
{
    Task<LoginForResultDto> AuthenticateAsync(string email, string password);
}
