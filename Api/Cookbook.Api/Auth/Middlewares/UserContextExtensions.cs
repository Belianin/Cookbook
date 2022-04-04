using System;
using Microsoft.AspNetCore.Http;

namespace Cookbook.Api.Auth.Middlewares;

public static class UserContextExtensions
{
    public static bool TryGetUserId(this HttpContext content, out Guid userId)
    {
        var result = content.Items.TryGetValue(AuthConsts.UserIdItemName, out var userIdObj);

        userId = result ? (Guid) userIdObj : Guid.Empty;
        
        return result;
    }
    
    public static Guid? GetUserId(this HttpContext content)
    {
        if (content.Items.TryGetValue(AuthConsts.UserIdItemName, out var userIdObj))
            return (Guid) userIdObj;

        return null;
    } 
}