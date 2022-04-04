using System;

namespace Cookbook.Api.Controllers.Recipes.Models;

public class CommentRequest
{
    public string Content { get; set; }
    public Guid? ReplyTo { get; set; }
}