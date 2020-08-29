using IMDb.Data.Core;
using IMDb.Data.Repositories;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
