using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Comments;

public interface ICommentsRepository
{
    Task<ICollection<Comment>> GetCommentsAsync(Guid contentId);
    Task AddCommentAsync(Comment comment);
}