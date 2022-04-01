using System;
using System.Threading.Tasks;

namespace Cookbook.Users;

public interface IUsersRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(Guid userId);
    Task SaveUserAsync(User user);
}