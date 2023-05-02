using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.DTOs.Expenses;

namespace MoneyManagement.Service.Interfaces;

public interface IExpenseService
{
    Task<ExpenseForResultDto> AddAsync(ExpenseForCreationDto dto);
    Task<ExpenseForResultDto> AddValue(string name, double value);
    Task<ExpenseForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<ExpenseForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<bool> RemoveAsync(int id);
    Task<bool> DestroyAsync(int id);
    Task<ExpenseForResultDto> ModifyOwnInfoAsync(ExpenseForUpdateDto dto);
    Task<ExpenseForResultDto> ModifyAsAdminAsync(int id, ExpenseForUpdateDto dto);
}
