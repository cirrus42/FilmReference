using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers;
using FilmReference.FrontEnd.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace FilmReference.FrontEnd.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IImageHelper, ImageHelper>();
            services.AddTransient<IPersonHandler, PersonHandler>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<Person>, GenericRepository<Person>>();
        }
    }
}
