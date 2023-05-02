using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.DAL.IRepositories;
using MoneyManagement.Domain.Configurations;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Expenses;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Extensions;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Shared.Helpers;

namespace MoneyManagement.Service.Services;

public class ExpenseService : IExpenseService
{
    private readonly IMapper mapper;
    private readonly IRepository<Expense> repository;

    public ExpenseService(IMapper mapper, IRepository<Expense> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public ExpenseService() { }
    public async Task<ExpenseForResultDto> AddAsync(ExpenseForCreationDto dto)
    {
        var existExpense = await this.repository.SelectAsync(e => e.Name.ToLower().Equals(dto.Name.ToLower()) &&
        e.UserId.Equals(HttpContextHelper.UserId));

        if (existExpense is not null && !existExpense.IsDeleted)
            throw new CustomException(404, "Expense is already exist");
        
        var mapped = this.mapper.Map<Expense>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var addedModel = await this.repository.InsertAsync(mapped);

        await this.repository.SaveChangesAsync();

        return this.mapper.Map<ExpenseForResultDto>(addedModel);
    }

    public async Task<bool> DestroyAsync(int id)
    {
        var existExpense = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "Expense not found");

        await this.repository.DeleteAsync(existExpense);
        await this.repository.SaveChangesAsync();
        return true;
    }

    public async Task<ExpenseForResultDto> ModifyAsAdminAsync(int id, ExpenseForUpdateDto dto)
    {
        var existExpense = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "Expense not found");
        this.mapper.Map(dto, existExpense);
        existExpense.LastUpdatedAt = DateTime.UtcNow;
        existExpense.UpdatedBy = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<ExpenseForResultDto>(existExpense);
    }

    public async Task<ExpenseForResultDto> ModifyOwnInfoAsync(ExpenseForUpdateDto dto)
    {
        var userId = HttpContextHelper.UserId;
        var existExpense = await this.repository.SelectAsync(u => u.Id.Equals(userId));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "Expense not found");
        this.mapper.Map(dto, existExpense);
        existExpense.LastUpdatedAt = DateTime.UtcNow;
        existExpense.UpdatedBy = userId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<ExpenseForResultDto>(existExpense);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var existExpense = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "Expense not found");
        existExpense.IsDeleted = true;
        existExpense.DeletedBy = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ExpenseForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var expenses = await this.repository.SelectAll()
            .Where(u => u.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ExpenseForResultDto>>(expenses);
    }

    public async Task<ExpenseForResultDto> RetrieveByIdAsync(int id)
    {
        var existExpense = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "User not found");

        return this.mapper.Map<ExpenseForResultDto>(existExpense);
    }

    public async Task<ExpenseForResultDto> AddValue(string name, double value)
    {
        var existExpense = await this.repository.SelectAsync(e => e.UserId.Equals(HttpContextHelper.UserId) &&
        e.Name.ToLower().Equals(name.ToLower()));
        if (existExpense is null || existExpense.IsDeleted)
            throw new CustomException(404, "Expense not found");

        existExpense.Value += value;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<ExpenseForResultDto>(existExpense);
    }
}
