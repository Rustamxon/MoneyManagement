using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.Service.DTOs.Users;

public class UserForChangePasswordDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter valid email address")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Old password must not be null or empty!")]
    public string OldPassword { get; set; }


    [Required(ErrorMessage = "New password must not be null or empty!")]
    public string NewPassword { get; set; }


    [Required(ErrorMessage = "Confirming password must not be null or empty!")]
    public string ConfirmNewPassword { get; set; }
}
