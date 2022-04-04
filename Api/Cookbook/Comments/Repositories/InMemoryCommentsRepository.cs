using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Comments.Repositories;

public class InMemoryCommentsRepository : ICommentsRepository
{
    private readonly Dictionary<Guid, List<Comment>> contentIdToComments = new();

    public Task<ICollection<Comment>> GetCommentsAsync(Guid contentId)
    {
        return Task.FromResult((ICollection<Comment>) contentIdToComments[contentId]);
    }

    public Task AddCommentAsync(Comment comment)
    {
        if (!contentIdToComments.ContainsKey(comment.ContentId))
            contentIdToComments[comment.ContentId] = new List<Comment>();
        contentIdToComments[comment.ContentId].Add(comment);
        
        return Task.CompletedTask;
    }
}