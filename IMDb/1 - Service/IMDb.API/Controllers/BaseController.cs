using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IMDb.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }
        protected List<string> GetErrorListFromModelState()
        {
            var errors = new List<string>();
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                errors.Add(errorMsg);
            }
            return errors;
        }

        protected IActionResult GetResponse()
        {
            return GetResponse(null);
        }

        protected IActionResult GetResponse(object result)
        {
            if (_notifications != null && _notifications.HasNotifications())
            {
                return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
            }

            if (result == null) return Ok();

            return Ok(result);
        }
    }
}
