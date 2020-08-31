using AutoMapper;
using IMDb.Application.ViewModels.Movie.Add;
using IMDb.Application.ViewModels.User;
using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Commands.User;
using IMDb.Domain.Entities;
using System;

namespace IMDb.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AddCastViewModel, Cast>()
                .ConstructUsing(m => new Cast(Guid.NewGuid(), m.Name));

            CreateMap<AddCastViewModel, AddCastCommand>()
                .ConstructUsing(c => new AddCastCommand(c.Name, c.CastType));

            CreateMap<AddMovieViewModel, AddMovieCommand>()
                .ConstructUsing(m => new AddMovieCommand(m.Title, m.Genre, m.CastIds));

            CreateMap<AddRatingOfMovieViewModel, AddRatingOfMovieCommand>()
                .ConstructUsing(m => new AddRatingOfMovieCommand(m.Rate, m.MovieId));

            //User
            CreateMap<AddUserViewModel, AddUserCommand>()
              .ConstructUsing(c => new AddUserCommand(c.Name, c.Password, c.RoleId, c.Status));

            CreateMap<UpdateUserViewModel, UpdateUserCommand>()
              .ConstructUsing(c => new UpdateUserCommand(c.Name, c.Password, c.Id.Value));
        }
    }
}
