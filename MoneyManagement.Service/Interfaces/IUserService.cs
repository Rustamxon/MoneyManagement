using MoneyManagement.Domain.Configurations;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Users;

namespace MoneyManagement.Service.Interfaces;

public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<bool> RemoveAsync(int id);
    Task<bool> DestroyAsync(int id);
    Task<UserForResultDto> ModifyOwnInfoAsync(UserForUpdateDto dto);
    Task<UserForResultDto> ModifyAsAdminAsync(int id, UserForUpdateDto dto);
    Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto);
    Task<User> RetrieveByEmailAsync(string email);
    Task<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto);
    Task<UserImageForResultDto> GetUserImageAsync(int userId);
    Task<bool> DeleteUserImageAsync(int userId);
}
