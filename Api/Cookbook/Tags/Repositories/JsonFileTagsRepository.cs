using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cookbook.Common;
using Newtonsoft.Json;

namespace Cookbook.Tags.Repositories;

public class JsonFileTagsRepository : ITagsRepository
{
    private readonly SharedDirectoryWatcher directoryWatcher;
    private List<Tag> tags;

    public JsonFileTagsRepository(SharedDirectoryWatcher directoryWatcher, string filename)
    {
        this.directoryWatcher = directoryWatcher;
        directoryWatcher.Subscribe(filename, x => tags = ReadFile(x));
        tags = ReadFile(Path.Combine(directoryWatcher.Directory, filename));
    }

    public Task<ICollection<Tag>> GetAllAsync()
    {
        return Task.FromResult((ICollection<Tag>) tags);
    }

    private static List<Tag> ReadFile(string fullPath)
    {
        var text = File.ReadAllText(fullPath);

        return JsonConvert.DeserializeObject<List<Tag>>(text);
    }
}