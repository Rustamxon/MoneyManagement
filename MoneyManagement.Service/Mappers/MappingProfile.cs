using AutoMapper;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Expenses;
using MoneyManagement.Service.DTOs.Users;

namespace MoneyManagement.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<UserForCreationDto, UserForUpdateDto>().ReverseMap();

        // Expense
        CreateMap<Expense, ExpenseForCreationDto>().ReverseMap();
        CreateMap<Expense, ExpenseForResultDto>().ReverseMap();
        CreateMap<Expense, ExpenseForUpdateDto>().ReverseMap();
        CreateMap<ExpenseForCreationDto, ExpenseForUpdateDto>().ReverseMap();
    }
}
