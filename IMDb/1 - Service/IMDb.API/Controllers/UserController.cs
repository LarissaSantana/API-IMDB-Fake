using IMDb.Application.Interfaces;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

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
    }
}
