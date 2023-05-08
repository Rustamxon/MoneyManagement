using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs.Login;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(LoginDto dto)
        {
            return Ok(await this.authService.AuthenticateAsync(dto.Email, dto.Password));
        }
    }
}
