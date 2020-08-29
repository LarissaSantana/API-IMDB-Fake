using IMDb.Application.Services;
using IMDb.Application.ViewModels.Add;
using Microsoft.AspNetCore.Mvc;

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
            _movieAppService.AddMovie(viewModel);
            return Ok();
        }
    }
}
