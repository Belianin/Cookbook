using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cookbook.Tags.Repositories;

public class JsonFileTagsRepository : ITagsRepository
{
    private readonly string fullPath;
    private FileSystemWatcher fileWatcher;
    private List<Tag> tags;

    public JsonFileTagsRepository(string directory, string filename)
    {
        directory = directory ?? throw new ArgumentNullException(nameof(directory));

        fullPath = Path.Combine(directory, filename);
        if (!Directory.Exists(directory))
            throw new DirectoryNotFoundException(directory);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException(filename);
        
        fileWatcher = new FileSystemWatcher(directory);
        fileWatcher.Changed += (_, e) =>
        {
            if (e.Name == filename)
                ReadFile();
        };
    }

    public Task<ICollection<Tag>> GetAllAsync()
    {
        if (tags == null)
            ReadFile();

        return Task.FromResult((ICollection<Tag>) tags);
    }

    private void ReadFile()
    {
        var text = File.ReadAllText(fullPath);

        tags = JsonConvert.DeserializeObject<List<Tag>>(text);
    }
}