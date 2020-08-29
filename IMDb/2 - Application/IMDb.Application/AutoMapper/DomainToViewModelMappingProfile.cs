using AutoMapper;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Core.Pagination;
using IMDb.Domain.Entities;

namespace IMDb.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {

            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.CastOfMovie, opt => opt.MapFrom(src => src.CastOfMovies));

            CreateMap<Cast, CastViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CastType, opt => opt.MapFrom(src => src.CastType));

            CreateMap<CastOfMovie, CastOfMovieViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Movie, MovieWithRatingViewModel>()
                .ForMember(dest => dest.CastOfMovie, opt => opt.MapFrom(src => src.CastOfMovies))
                .ForMember(dest => dest.NumberOfVotes, opt => opt.MapFrom(src => src.RatingOfMovies.Count));

            CreateMap<Pagination<Movie>, Pagination<MovieWithRatingViewModel>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
