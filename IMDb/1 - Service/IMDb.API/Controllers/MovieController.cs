using IMDb.Application.Services;
using IMDb.Application.ViewModels.Add;
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

        public MovieController(IMovieAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] AddMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddMovie(viewModel);
            return Ok();
        }

        [HttpPost]
        [Route("movieRating")]
        public IActionResult CreateMovieRating([FromBody] AddRatingOfMovieViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _movieAppService.AddRatingOfMovie(viewModel);
            return Ok();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetMovieById(Guid id)
        {
            var movie = _movieAppService.GetMovieById(id);
            return Ok(movie);
        }

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
    }
}
