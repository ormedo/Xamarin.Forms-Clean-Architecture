using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using my_clean_way.data;
using my_clean_way.movies.domain;
using System.Reactive.Linq;
using System.Linq;
using System.Diagnostics;

namespace my_clean_way.movie_favorites.repository
{
    public class FavoriteMovies : IFavoriteMovieRepository
    {
        public FavoriteMovies()
        {
            BlobCache.ApplicationName = DBUtils.DB_NAME;
        }

        public async Task<Movie> AddFavorite(Movie movie)
        {
            await BlobCache.LocalMachine.InsertObject<Movie>(movie.Id.ToString(), movie);
            return movie;
        }

        public async Task<List<Movie>> GetFavoriteMovies()
        {
            var storedObjects = await BlobCache.LocalMachine.GetAllObjects<Movie>();
            return new List<Movie>(storedObjects);
        }

        public async Task<bool> IsMovieFavorite(Movie movie)
        {
            var result = false;
            try{
                var stored = await BlobCache.LocalMachine.GetObject<Movie>(movie.Id.ToString())
                                    .Catch(Observable.Return( default(Movie)));
                if(stored != null) {
                    result = stored.IsFavorite;
                }
            }
            catch(KeyNotFoundException){
                result = false;
            }
            return result;          
        }

        public async Task<bool> RemoveFavorite(Movie movie)
        {
            await BlobCache.LocalMachine.InvalidateObject<Movie>(movie.Id.ToString());
            return true;
        }
    }
}
