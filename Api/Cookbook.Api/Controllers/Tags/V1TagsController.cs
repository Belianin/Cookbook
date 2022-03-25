using System.Threading.Tasks;
using Cookbook.Tags.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Api.Controllers.Tags;

[Route("v1/tags")]
public class V1TagsController : ControllerBase
{
    private readonly ITagsRepository tagsRepository;

    public V1TagsController(ITagsRepository tagsRepository)
    {
        this.tagsRepository = tagsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await tagsRepository.GetAllAsync();
        
        return Ok(tags);
    }
}