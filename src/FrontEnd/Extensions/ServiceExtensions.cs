using FilmReference.FrontEnd.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace FilmReference.FrontEnd.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IImageHelper, ImageHelper>();
        }
    }
}
