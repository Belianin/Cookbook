using System;
using System.Collections.Generic;

namespace Cookbook.Api.Controllers.Recipes.Models;

public class CommentResponse
{
    public Guid Id { get; set; }
    public ICollection<CommentResponse> Replies { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Content { get; set; }
    public int Mark { get; set; }
    public bool? UserLiked { get; set; }
}