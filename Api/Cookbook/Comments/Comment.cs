using System;
using System.Collections.Generic;

namespace Cookbook.Comments;

public class Comment
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid? ReplyTo { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Content { get; set; }
    public ICollection<Mark> Marks { get; set; }
}