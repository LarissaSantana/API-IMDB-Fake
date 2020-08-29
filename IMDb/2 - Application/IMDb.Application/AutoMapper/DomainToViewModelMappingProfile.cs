using AutoMapper;
using IMDb.Application.ViewModels;
using IMDb.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IMDb.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        Func<Movie, MovieViewModel> lambda = m =>
        {
            var castOfMovieList = new List<CastOfMovieViewModel>();
            if (m.CastOfMovies != null)
            {
                foreach (var castOfMovie in m.CastOfMovies)
                {
                    castOfMovieList.Add(new CastOfMovieViewModel
                    {
                        CastOfMovieId = castOfMovie.Id,
                        Name = castOfMovie.Cast.Name,
                        CastType = castOfMovie.Cast.CastType
                    });
                }
            }

            return new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                Mean = m.Mean,
                CastOfMovieList = castOfMovieList
            };
        };

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>()
                .ConstructUsing(m => lambda(m));
        }
    }
}
