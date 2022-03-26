using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cookbook.Recipes.Repositories;

public class JsonFileRecipesRepository : IRecipesRepository
{
    private readonly string fullPath;
    private FileSystemWatcher fileWatcher;
    private Dictionary<Guid, Recipe> recipes;

    public JsonFileRecipesRepository(string directory, string filename)
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
    
    public Task<ICollection<Recipe>> GetAllAsync()
    {
        if (recipes == null)
            ReadFile();

        return Task.FromResult((ICollection<Recipe>) recipes.Values);
    }

    public Task<Recipe> GetByIdAsync(Guid id)
    {
        return Task.FromResult(recipes[id]);
    }
    
    private void ReadFile()
    {
        var text = File.ReadAllText(fullPath);

        recipes = JsonConvert.DeserializeObject<Recipe[]>(text)!.ToDictionary(x => x.Id, y => y);
    }
}