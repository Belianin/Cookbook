using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Api.Auth.Middlewares;
using Cookbook.Api.Controllers.Recipes.Models;
using Cookbook.Comments;
using Cookbook.Recipes.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Api.Controllers.Recipes;

[Route("v1/recipes")]
public class V1RecipesController : ControllerBase
{
    private readonly IRecipesRepository recipesRepository;
    private readonly ICommentsRepository commentsRepository;

    public V1RecipesController(
        IRecipesRepository recipesRepository,
        ICommentsRepository commentsRepository)
    {
        this.recipesRepository = recipesRepository;
        this.commentsRepository = commentsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await recipesRepository.GetAllAsync();

        return Ok(result.Select(x => RecipeResponse.FromValue(x)).ToArray());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await recipesRepository.GetByIdAsync(id);

        return Ok(RecipeResponse.FromValue(result));
    }

    [UserContext]
    [HttpGet("{id:guid}/comments")]
    public async Task<IActionResult> GetComments([FromRoute] Guid id)
    {
        var userId = HttpContext.GetUserId();
        
        var comments = await commentsRepository.GetCommentsAsync(id).ConfigureAwait(false);

        var commentIdToRepliesIds = new Dictionary<Guid, List<Comment>>();

        foreach (var comment in comments)
        {
            if (comment.ReplyTo != null)
            {
                if (!commentIdToRepliesIds.ContainsKey(comment.ReplyTo.Value))
                    commentIdToRepliesIds[comment.ReplyTo.Value] = new List<Comment>();
                commentIdToRepliesIds[comment.ReplyTo.Value].Add(comment);
            }
        }

        CommentResponse ConvertComment(Comment comment)
        {
            CommentResponse[] repliesResponses = null;
            if (commentIdToRepliesIds.TryGetValue(comment.Id, out var replies))
                repliesResponses = replies
                    .Select(x => ConvertComment(x))
                    .OrderBy(x => x.CreatedDate)
                    .ToArray();

            bool? userLiked = null; // todo
            var mark = 0;

            foreach (var commentMark in comment.Marks)
            {
                mark += commentMark.IsPositive ? 1 : -1;
                
                if (commentMark.UserId == userId) 
                    userLiked = commentMark.IsPositive;
            }

            return new CommentResponse
            {
                Content = comment.Content,
                Replies = repliesResponses,
                Id = comment.Id,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                Mark = mark,
                UserLiked = userLiked
            };
        }
        
        var result = new CommentsResponse
        {
            Comments = comments
                .Where(x => x.ReplyTo == null)
                .Select(x => ConvertComment(x))
                .OrderBy(x => x.CreatedDate)
                .ToArray()
        };

        return Ok(result);
    }

    [UserContext]
    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> AddComment([FromRoute] Guid recipeId, [FromBody] CommentRequest request)
    {
        var userId = HttpContext.GetUserId();
        if (userId == null)
            return Unauthorized();

        var recipe = await recipesRepository.GetByIdAsync(recipeId).ConfigureAwait(false);
        if (recipe == null)
            return NotFound();
        
        // todo if reply to not exists

        var comment = new Comment
        {
            Content = request.Content,
            ContentId = recipeId,
            CreatedDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Marks = Array.Empty<Mark>(),
            ReplyTo = request.ReplyTo,
            UserId = userId.Value
        };

        await commentsRepository.AddCommentAsync(comment);

        return Ok();
    }
}