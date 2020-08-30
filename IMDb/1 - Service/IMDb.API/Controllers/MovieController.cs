using IMDb.Application.Services.Interfaces;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IMDb.API.Controllers
{
    //TODO: fazer versionamento
    [Route("api/movie")]
    public class MovieController : BaseController
    {
        private readonly IMovieAppService _movieAppService;

        public MovieController(IMovieAppService movieAppService,
             IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _movieAppService = movieAppService;
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] AddMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddMovie(viewModel);
            return GetResponse();
        }

        [HttpPost]
        [Route("movieRating")]
        public IActionResult AddRatingOfMovie([FromBody] AddRatingOfMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddRatingOfMovie(viewModel);
            return GetResponse();
        }

        [HttpPost]
        [Route("cast")]
        public IActionResult AddCast([FromBody] AddCastViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddCast(viewModel);
            return GetResponse();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetMovieById(Guid id)
        {
            var movie = _movieAppService.GetMovieById(id);
            return GetResponse(movie);
        }

        [HttpGet]
        [Route("moviesByFilters")]
        public IActionResult GetMovieByFilters([FromBody] MovieFilterViewModel viewModel, int pageNumber = 1, int pageSize = 10)
        {
            var movies = _movieAppService.GetMoviesWithPagination(viewModel, pageNumber, pageSize);

            return GetResponse(movies);
        }      
    }
}
