using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Tags.Repositories;

public interface ITagsRepository
{
    Task<ICollection<Tag>> GetAllAsync();
}