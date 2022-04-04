using System.Collections.Generic;

namespace Cookbook.Api.Controllers.Recipes.Models;

public class CommentsResponse
{
    public ICollection<CommentResponse> Comments { get; set; }
}