using Microsoft.AspNetCore.Http;

namespace MoneyManagement.Service.DTOs.Users;

public class UserImageForCreationDto
{
    public IFormFile Image { get; set; }
    public int UserId { get; set; }
}
