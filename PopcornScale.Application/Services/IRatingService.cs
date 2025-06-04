namespace PopcornScale.Application.Services;

public interface IRatingService
{
    Task<bool> RateMovieAsync(Guid movieId, int rating, Guid useId, CancellationToken token = default);
}
