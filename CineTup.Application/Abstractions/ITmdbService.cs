namespace CineTup.Application.Abstractions
{
    public interface ITmdbService
    {
        Task<string?> GetRandomMoviePosterAsync();
    }
}
