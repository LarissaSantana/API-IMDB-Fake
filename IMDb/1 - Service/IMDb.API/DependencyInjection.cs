using IMDb.Domain.Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
