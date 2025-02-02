﻿using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels.User;
using IMDb.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IMDb.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService,
             IDomainNotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] AddUserViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _userAppService.AddUser(viewModel);
            return GetResponse();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserViewModel viewModel)
        {
            var errors = GetErrorListFromModelState();
            if (errors.Any())
                return BadRequest(errors);

            _userAppService.UpdateUser(viewModel);
            return GetResponse();
        }

        [HttpPut]
        [Route("deactivate")]
        public IActionResult DeactivateUser(Guid id)
        {
            _userAppService.ChangeStatus(id, false);
            return GetResponse();
        }

        [HttpPut]
        [Route("activate")]
        public IActionResult ActivateUser(Guid id)
        {
            _userAppService.ChangeStatus(id, true);
            return GetResponse();
        }

        [HttpGet]
        [Authorize(Roles = "e33a5da4-4c46-4f0e-8ef7-8d01a12f9884")]
        [Route("getNonActiveCommonUsers")]
        public IActionResult GetNonActiveCommonUsers(int pageNumber = 1, int pageSize = 10)
        {
            var usersPagination = _userAppService.GetNonActiveCommonUsers(pageNumber, pageSize);
            return GetResponse(usersPagination);
        }
    }
}
