using Microsoft.AspNetCore.Http;

namespace MoneyManagement.Shared.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor.HttpContext;
    public static int UserId => int.Parse(HttpContext.User.FindFirst("Id").Value);
    public static string UserRole => HttpContext.User.FindFirst("Role").Value;
}
