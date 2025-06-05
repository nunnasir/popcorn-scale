using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopcornScale.Api.Auth;
using PopcornScale.Api.Mapping;
using PopcornScale.Application.Services;
using PopcornScale.Contracts.Requests;

namespace PopcornScale.Api.Controllers;

[ApiController]
[Route("")]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [Authorize]
    [HttpPut(ApiEndPoints.Movies.Rate)]
    public async Task<IActionResult> RateMovie([FromRoute] Guid id,
        [FromBody] RateMovieRequest request, CancellationToken token)
    {
        var userId = HttpContext.GetUserId();
        var result = await _ratingService.RateMovieAsync(id, request.Rating, userId!.Value, token);

        return result ? Ok() : NotFound();
    }

    [Authorize]
    [HttpDelete(ApiEndPoints.Movies.DeleteRating)]
    public async Task<IActionResult> DeleteRating([FromRoute] Guid id, CancellationToken token)
    {
        var userId = HttpContext.GetUserId();
        var result = await _ratingService.DeleteRatingAsync(id, userId!.Value, token);

        return result ? Ok() : NotFound();
    }

    [Authorize]
    [HttpGet(ApiEndPoints.Ratings.GetUserRatings)]
    public async Task<IActionResult> GetUserRatings(CancellationToken token)
    {
        var userId = HttpContext.GetUserId();
        var ratings = await _ratingService.GetRatingsForUserAsync(userId!.Value, token);
        var ratingResponse = ratings.MapToResponse();

        return Ok(ratingResponse);
    }
}
