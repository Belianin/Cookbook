using System;

namespace Cookbook.Users;

public static class UserFactory
{
    public static bool TryCreate(string username, string password, out User user)
    {
        user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = PasswordHasher.Hash(password)
        };
        return true;
    }
}