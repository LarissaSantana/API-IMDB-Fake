using AutoMapper;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace IMDb.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        //Func<Movie, MovieViewModel> MovieViewModelLambda = m =>
        //{
        //    var castOfMovieList = new List<CastOfMovieViewModel>();
        //    if (m.CastOfMovies != null)
        //    {
        //        foreach (var castOfMovie in m.CastOfMovies)
        //        {
        //            castOfMovieList.Add(new CastOfMovieViewModel
        //            {
        //                CastOfMovieId = castOfMovie.Id,
        //                Name = castOfMovie.Cast.Name,
        //                CastType = castOfMovie.Cast.CastType
        //            });
        //        }
        //    }

        //    return new MovieViewModel
        //    {
        //        Id = m.Id,
        //        Title = m.Title,
        //        Genre = m.Genre,
        //        Mean = m.Mean,
        //        CastOfMovieList = castOfMovieList
        //    };
        //};

        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Movie, MovieViewModel>()
            //    .ConstructUsing(m => MovieViewModelLambda(m));

            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.CastOfMovie, opt => opt.MapFrom(src => src.CastOfMovies));

            CreateMap<Cast, CastViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CastType, opt => opt.MapFrom(src => src.CastType));

            CreateMap<CastOfMovie, CastOfMovieViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            //CreateMap<Movie, MovieWithRatingViewModel>()
            //    .ConstructUsing(m => MovieWithRatingViewModelLambda(m));
        }
    }
}
