using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopcornScale.Api.Mapping;
using PopcornScale.Application.Services;
using PopcornScale.Contracts.Requests;

namespace PopcornScale.Api.Controllers;

[Authorize]
[ApiController]
[Route("")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost(ApiEndPoints.Movies.Create)]
    public async Task<IActionResult> Create(
        [FromBody] CreateMovieRequest request, 
        CancellationToken token)
    {
        var movie = request.MapToMovie();
        var movieResponse = movie.MapToResponse();
        await _movieService.CreateAsync(movie, token);

        return CreatedAtAction(nameof(Get), new { idOrSlug = movieResponse.Id }, movieResponse);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndPoints.Movies.Get)]
    public async Task<IActionResult> Get(
        [FromRoute] string idOrSlug, 
        CancellationToken token)
    {
        var movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieService.GetByIdAsync(id, token)
            : await _movieService.GetBySlugAsync(idOrSlug, token);

        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndPoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var movies = await _movieService.GetAllAsync(token);

        var response = movies.MapToResponse();
        return Ok(response);
    }

    [HttpPut(ApiEndPoints.Movies.Update)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] UpdateMovieRequest request,
        CancellationToken token)
    {
        var movie = request.MapToMovie(id);
        var updatedMovie = await _movieService.UpdateAsync(movie, token);

        if (updatedMovie is null)
        {
            return NotFound();
        }

        var response = updatedMovie.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndPoints.Movies.Delete)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id, 
        CancellationToken token)
    {
        var deleted = await _movieService.DeleteByIdAsync(id, token);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}
