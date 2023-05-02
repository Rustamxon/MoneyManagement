using MoneyManagement.DAL.IRepositories;
using MoneyManagement.DAL.Repositories;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.Services;


namespace Triangle.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<IAuthService, AuthService>();
    }   
}
