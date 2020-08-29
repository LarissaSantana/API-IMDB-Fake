using AutoMapper;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Commands;
using IMDb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDb.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        Func<AddMovieViewModel, AddMovieCommand> lambda = m =>
        {
            var castCommand = new List<AddCastCommand>();
            if (m.Casts != null)
                m.Casts.ToList().ForEach(c => castCommand.Add(new AddCastCommand(c.Name, c.CastType)));

            return new AddMovieCommand(m.Title, m.Genre, castCommand);
        };

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AddCastViewModel, Cast>()
                .ConstructUsing(m => new Cast(Guid.NewGuid(), m.Name));

            CreateMap<AddCastViewModel, AddCastCommand>()
                .ConstructUsing(c => new AddCastCommand(c.Name, c.CastType));

            CreateMap<AddMovieViewModel, AddMovieCommand>()
                .ConstructUsing(m => lambda(m));

            CreateMap<AddRatingOfMovieViewModel, AddRatingOfMovieCommand>()
                .ConstructUsing(m => new AddRatingOfMovieCommand(m.Rate, m.MovieId, m.UserId));
        }
    }
}
