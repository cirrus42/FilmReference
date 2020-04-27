using FilmReference.DataAccess.DbClasses;
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
            services.AddTransient<IPersonPagesManager, PersonPagesManager>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<PersonEntity>, GenericRepository<PersonEntity>>();
            services.AddTransient<IGenericRepository<GenreEntity>, GenericRepository<GenreEntity>>();
            services.AddTransient<IGenericRepository<StudioEntity>, GenericRepository<StudioEntity>>();
            services.AddTransient<IGenericRepository<FilmEntity>, GenericRepository<FilmEntity>>();
            services.AddTransient<IGenericRepository<FilmPersonEntity>, GenericRepository<FilmPersonEntity>>();
        }
    }
}