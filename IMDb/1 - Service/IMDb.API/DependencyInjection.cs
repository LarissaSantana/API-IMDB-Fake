using IMDb.Application.Interfaces;
using IMDb.Application.Services;
using IMDb.Application.Services.Interfaces;
using IMDb.Data.Core;
using IMDb.Data.Repositories;
using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Events;
using IMDb.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IMovieAppService, MovieAppService>();
            services.AddScoped<IUserAppService, UserAppService>();

            services.AddScoped<IRequestHandler<AddMovieCommand, bool>, MovieCommandHandler>();
            services.AddScoped<IRequestHandler<AddRatingOfMovieCommand, bool>, MovieCommandHandler>();
            services.AddScoped<IRequestHandler<AddMeanCommand, bool>, MovieCommandHandler>();
            services.AddScoped<IRequestHandler<AddCastCommand, bool>, MovieCommandHandler>();

            services.AddScoped<INotificationHandler<RatingOfMovieAddedEvent>, MovieEventHandler>();
        }
    }
}
