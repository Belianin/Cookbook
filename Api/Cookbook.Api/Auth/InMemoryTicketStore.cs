using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Cookbook.Api.Auth;

public class InMemoryTicketStore : ITicketStore
{
    private Dictionary<string, AuthenticationTicket> store = new Dictionary<string, AuthenticationTicket>();
    
    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString();

        store[key] = ticket;
        
        return Task.FromResult(key);
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        store[key] = ticket;

        return Task.CompletedTask;
    }

    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        if (store.TryGetValue(key, out var ticket))
            return Task.FromResult(ticket);

        return Task.FromResult<AuthenticationTicket>(null);
    }

    public Task RemoveAsync(string key)
    {
        store.Remove(key);

        return Task.CompletedTask;
    }
}