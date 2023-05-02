﻿using MoneyManagement.Shared.Helpers;

namespace MoneyManagement.Api.Extensions;

public static class HttpContextExtension
{
    public static void InitAccessor(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        HttpContextHelper.Accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
    }
}
