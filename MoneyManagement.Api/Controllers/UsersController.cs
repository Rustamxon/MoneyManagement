using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Api.Controllers;
//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("get-all")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await this.userService.RetrieveAllAsync(@params));

    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(int id)
            => Ok(await this.userService.RetrieveByIdAsync(id));

    [HttpPost("add")]
    [AllowAnonymous]
    public async ValueTask<ActionResult<UserForResultDto>> PostAsync(UserForCreationDto dto)
        => Ok(await this.userService.AddAsync(dto));

    [HttpPut("update")]
    public async ValueTask<ActionResult<UserForResultDto>> PutAsync(UserForUpdateDto dto)
        => Ok(await this.userService.ModifyOwnInfoAsync(dto));

    [HttpPut("update-as-admin")]
    //[Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<ActionResult<UserForResultDto>> PutAsAdminAsync(int id, UserForUpdateDto dto)
        => Ok(await this.userService.ModifyAsAdminAsync(id, dto));

    [HttpDelete("delete-by-id")]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.userService.RemoveAsync(id));

    [HttpDelete("delete-as-admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async ValueTask<ActionResult<bool>> DestroyAsync(int id)
        => Ok(await this.userService.DestroyAsync(id));

    [HttpPut("change-password")]
    public async ValueTask<ActionResult<UserForResultDto>> ChangePasswordAsync(UserForChangePasswordDto dto)
        => Ok(await this.userService.ChangePasswordAsync(dto));

    [HttpPost("image-upload")]
    public async ValueTask<IActionResult> UploadImage([FromForm] UserImageForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.ImageUploadAsync(dto)
        });

    [HttpDelete("image-delete/{userId:int}")]
    public async Task<IActionResult> DeleteUserImage(int userId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.DeleteUserImageAsync(userId)
        });

    [HttpGet("image-get/{userId:int}")]
    public async Task<IActionResult> GetUserImage(int userId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.GetUserImageAsync(userId)
        });
}
