using System;
using Prism;
using Prism.Ioc;
using Prism.DryIoc;
using Xamarin.Forms.Xaml;
using my_clean_way.movie_list.ui;
using my_clean_way.movie_list.repository;
using Refit;
using my_clean_way.data;
using my_clean_way.movie_favorites.repository;
using my_clean_way.domain;
using System.Collections.Generic;
using my_clean_way.movies.domain;
using my_clean_way.ui;
using my_clean_way.movies.ui;
using my_clean_way.movie_favorites.domain;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace my_clean_way
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new Uri("/" + RootView.Route + "/" +MovieListPage.Route, UriKind.Absolute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //TODO: Register your navigation and interfaces here
            containerRegistry.RegisterForNavigation<RootView>(RootView.Route);
            containerRegistry.RegisterForNavigation<MovieListPage, MovieListPageViewModel>(MovieListPage.Route);
            containerRegistry.RegisterForNavigation<MoviePage, MoviePageViewModel>(MoviePage.Route);


            //Interface registration
            containerRegistry.RegisterSingleton<IMovieListRepository, MovieListRepository>();
            containerRegistry.RegisterSingleton<IFavoriteMovieRepository, FavoriteMovies>();
            containerRegistry.RegisterInstance<IApiMovieDataSource>(RestService.For<IApiMovieDataSource>(ApiUtils.API_URL_BASE));
            containerRegistry.RegisterSingleton<IUseCase<List<Movie>, bool>, GetPopularMoviesUseCase>();
            containerRegistry.RegisterSingleton<IUseCase<Movie, Movie>, AddMovieToFavoriteUseCase>();
            containerRegistry.RegisterSingleton<IUseCase<bool, Movie>, IsMovieInFavoritesUseCase>();
        }
    }
}
