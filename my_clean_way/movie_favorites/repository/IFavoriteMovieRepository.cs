using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using my_clean_way.movies.domain;

namespace my_clean_way.movie_favorites.repository
{
    public interface IFavoriteMovieRepository
    {
        Task<List<Movie>> GetFavoriteMovies();
        Task<bool> IsMovieFavorite(Movie movie);
        Task<Movie> AddFavorite(Movie movie);
        Task<Boolean> RemoveFavorite(Movie movie);
    }
}
