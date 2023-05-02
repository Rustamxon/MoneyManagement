using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.DTOs.Expenses;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService expenseService;
    public ExpensesController(IExpenseService expenseService)
    {
        this.expenseService = expenseService;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await this.expenseService.RetrieveAllAsync(@params));


    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(int id)
        => Ok(await this.expenseService.RetrieveByIdAsync(id));


    [HttpPost("add")]
    [AllowAnonymous]
    public async ValueTask<ActionResult<ExpenseForResultDto>> PostAsync(ExpenseForCreationDto dto)
        => Ok(await this.expenseService.AddAsync(dto));


    [HttpPut("update")]
    public async ValueTask<ActionResult<ExpenseForResultDto>> PutAsync(ExpenseForUpdateDto dto)
        => Ok(await this.expenseService.ModifyOwnInfoAsync(dto));


    [HttpPut("update-as-admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<ActionResult<ExpenseForResultDto>> PutAsAdminAsync(int id, ExpenseForUpdateDto dto)
        => Ok(await this.expenseService.ModifyAsAdminAsync(id, dto));


    [HttpDelete("delete-by-id")]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.expenseService.RemoveAsync(id));


    [HttpDelete("delete-as-admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async ValueTask<ActionResult<bool>> DestroyAsync(int id)
        => Ok(await this.expenseService.DestroyAsync(id));


    [HttpPut("add-value")]
    public async ValueTask<ActionResult<ExpenseForResultDto>> AddValue(string name, double value)
        => Ok(await this.expenseService.AddValue(name, value));

}
