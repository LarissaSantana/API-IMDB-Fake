using IMDb.Application.Services;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDb.API.Controllers
{
    //TODO: fazer versionamento
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieAppService _movieAppService;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public MovieController(IMovieAppService movieAppService,
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _movieAppService = movieAppService;
            _notifications = notifications;
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

        //TODO: criar uma classe base
        private List<string> GetErrorListFromModelState()
        {
            var errors = new List<string>();
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                errors.Add(errorMsg);
            }
            return errors;
        }

        private IActionResult GetResponse()
        {
            return GetResponse(null);
        }

        private IActionResult GetResponse(object result)
        {
            if (_notifications.HasNotifications())
            {
                return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
            }

            if (result == null) return Ok();

            return Ok(result);
        }
    }
}
