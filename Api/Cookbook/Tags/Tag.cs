namespace Cookbook.Tags;

public class Tag
{
    public string Category { get; }
    public string Id { get; }
    public string Description { get; }

    public Tag(string category, string id, string description)
    {
        Category = category;
        Id = id;
        Description = description;
    }
}