using System;
using System.Security.Cryptography;
using System.Text;

namespace Cookbook.Users;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        return Convert.ToBase64String(
            MD5.HashData(
                Encoding.UTF8.GetBytes(password + "salt")));
    }
}