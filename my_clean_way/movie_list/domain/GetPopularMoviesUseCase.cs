using System;
using my_clean_way.movies.domain;
using my_clean_way.domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using my_clean_way.movie_list.repository;

namespace my_clean_way.movies.domain
{
    public class GetFavoriteMoviesUseCase : IUseCase<List<Movie>,bool>
    {
        readonly IMovieListRepository _movieListRepository;
        public GetFavoriteMoviesUseCase(IMovieListRepository movieListRepository)
        {
            _movieListRepository = movieListRepository;
        }

        public async Task<Transaction<List<Movie>>> execute(bool needMoreItems)
        {
            var transaction = new Transaction<List<Movie>>();
            try{
                transaction.Result = await _movieListRepository.GetPopularMovies(needMoreItems);
                transaction.Status = TransactionStatus.Success;
            }catch(Exception ex) {
                transaction.ErrorMessage = ex.Message;
                transaction.Status = TransactionStatus.Failure;
            }

            return transaction;
        }
    }
}
