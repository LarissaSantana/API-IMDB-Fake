using AutoMapper;
using IMDb.Application.ViewModels.Movie.Return;
using IMDb.Application.ViewModels.User;
using IMDb.Domain.Entities;
using IMDb.Domain.Utility;

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

            //TODO: criar uma classe partial
            //User
            CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<Pagination<User>, Pagination<UserViewModel>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
