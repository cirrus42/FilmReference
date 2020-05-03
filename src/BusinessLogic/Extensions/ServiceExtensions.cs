using BusinessLogic.Handlers;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Helpers;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Validations;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
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
            services.AddTransient<IPersonValidator, PersonValidator>();
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