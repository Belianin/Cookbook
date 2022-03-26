namespace Cookbook.Common;

public class SharedDirectoryWatcher
{
    public string Directory { get; }
    
    private readonly Dictionary<string, Action<string>> subscribers = new();
    private readonly FileSystemWatcher fileWatcher;

    public SharedDirectoryWatcher(string directory)
    {
        this.Directory = directory ?? throw new ArgumentNullException(nameof(directory));
        
        fileWatcher = new FileSystemWatcher(directory);
        fileWatcher.Changed += (_, e) =>
        {
            if (e.Name == null)
                return;

            if (subscribers.TryGetValue(e.Name, out var callback))
                callback(Path.Combine(directory, e.Name));
        };
    }

    public void Subscribe(string filename, Action<string> action)
    {
        var fullPath = Path.Combine(Directory, filename);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException(filename);

        subscribers[filename] = action;
    }
}