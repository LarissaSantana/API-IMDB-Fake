using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDb.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public AuthenticationController(IAuthenticationAppService authenticationAppService) : base(null)
        {
            _authenticationAppService = authenticationAppService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserLoginViewModel viewModel)
        {
            string error;
            var token = _authenticationAppService.Authenticate(viewModel, out error);

            if (!string.IsNullOrWhiteSpace(error))
                return NotFound(new { message = error });

            return GetResponse(new { token = token });
        }
    }
}
