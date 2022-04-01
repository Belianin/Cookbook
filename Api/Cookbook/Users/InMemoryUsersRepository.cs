using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Users;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly Dictionary<Guid, User> users = new();
    
    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.FromResult(users.Values.FirstOrDefault(x => x.Username == username));
    }

    public Task<User?> GetByIdAsync(Guid userId)
    {
        var result = users.TryGetValue(userId, out var user)
            ? user
            : null;
        
        return Task.FromResult(result);
    }

    public Task SaveUserAsync(User user)
    {
        users[user.Id] = user;
        
        return Task.CompletedTask;
    }
}