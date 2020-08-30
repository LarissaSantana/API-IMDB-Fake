using IMDb.Application.Services.Interfaces;
using IMDb.Application.ViewModels.Movie;
using IMDb.Application.ViewModels.Movie.Add;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IMDb.API.Controllers
{
    //TODO: fazer versionamento
    [Authorize]
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
        [Authorize(Roles = "e33a5da4-4c46-4f0e-8ef7-8d01a12f9884")]
        public IActionResult AddMovie([FromBody] AddMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddMovie(viewModel);
            return GetResponse();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult teste()
        {

        }

        [HttpPost]
        [Route("cast")]
        [Authorize(Roles = "e33a5da4-4c46-4f0e-8ef7-8d01a12f9884")]
        public IActionResult AddCast([FromBody] AddCastViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddCast(viewModel);
            return GetResponse();
        }

        [HttpPost]
        [Route("ratingOfMovie")]
        [Authorize(Roles = "e33a5da4-4c46-4f0e-8ef7-8d01a12f9884")]
        public IActionResult AddRatingOfMovie([FromBody] AddRatingOfMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddRatingOfMovie(viewModel);
            return GetResponse();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [AllowAnonymous]
        public IActionResult GetMovieById(Guid id)
        {
            var movie = _movieAppService.GetMovieById(id);
            return GetResponse(movie);
        }

        [HttpGet]
        [Route("moviesByFilters")]
        [AllowAnonymous]
        public IActionResult GetMovieByFilters([FromBody] MovieFilterViewModel viewModel, int pageNumber = 1, int pageSize = 10)
        {
            var movies = _movieAppService.GetMovies(viewModel, pageNumber, pageSize);

            return GetResponse(movies);
        }      
    }
}
