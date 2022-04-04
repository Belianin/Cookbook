using System;
using System.Linq;

namespace Cookbook.Api.Auth;

public static class SidGenerator
{
    public static string GenerateSid()
    {
        var bytes = Guid.NewGuid().ToByteArray()
            .Concat(Guid.NewGuid().ToByteArray())
            .Concat(Guid.NewGuid().ToByteArray())
            .Concat(Guid.NewGuid().ToByteArray())
            .ToArray();

        return Convert.ToBase64String(bytes);
    }
}