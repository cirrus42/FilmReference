using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FilmReference.FrontEnd.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IImageHelper, ImageHelper>();
            services.AddTransient<IPersonHandler, PersonHandler>();
            services.AddTransient<IGenreHandler, GenreHandler>();
            services.AddTransient<IStudioHandler, StudioHandler>();
            services.AddTransient<IFilmHandler, FilmHandler>();
            services.AddTransient<IFilmPersonHandler, FilmPersonHandler>();
            services.AddTransient<IFilmPagesManager, FilmPagesManager>();
            services.AddTransient<IGenrePagesManager, GenrePagesManager>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<Person>, GenericRepository<Person>>();
            services.AddTransient<IGenericRepository<Genre>, GenericRepository<Genre>>();
            services.AddTransient<IGenericRepository<Studio>, GenericRepository<Studio>>();
            services.AddTransient<IGenericRepository<Film>, GenericRepository<Film>>();
            services.AddTransient<IGenericRepository<FilmPerson>, GenericRepository<FilmPerson>>();
        }
    }
}