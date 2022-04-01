using System;
using System.Collections.Generic;

namespace Cookbook.Api.Auth;

public class SessionStore
{
    private readonly Dictionary<Guid, string> userToSession = new();
    private readonly Dictionary<string, Guid> sessionToUser = new();

    public void Set(string sessionId, Guid userId)
    {
        userToSession[userId] = sessionId;
        sessionToUser[sessionId] = userId;
    }

    public Guid? Get(string sessionId)
    {
        if (sessionToUser.TryGetValue(sessionId, out var userId))
            return userId;

        return null;
    }
}