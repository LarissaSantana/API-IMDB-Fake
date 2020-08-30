using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IMDb.API.Controllers
{
    //TODO: versionamento
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService,
             IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _userAppService.AddUser(viewModel);
            return GetResponse();
        }
    }
}
