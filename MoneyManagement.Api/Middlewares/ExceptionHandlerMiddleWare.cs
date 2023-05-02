using MoneyManagement.Api.Models;
using MoneyManagement.Service.Exceptions;

namespace MoneyManagement.Api.Middlewares;

public class ExceptionHandlerMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleWare> logger;

    public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (CustomException exception)
        {
            context.Response.StatusCode = exception.Code;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = exception.Code,
                Error = exception.Message
            });
        }
        catch (Exception exception)
        {
            this.logger.LogError($"{exception}\n\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Error = exception.Message
            });
        }
    }
}
