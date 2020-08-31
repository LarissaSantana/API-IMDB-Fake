using Microsoft.AspNetCore.Http;
using System;

namespace IMDb.Data.CrossCutting
{
    public class AuthenticatedUser
    {
        private readonly IHttpContextAccessor _accesor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accesor = accessor;
        }

        public Guid Id => Guid.Parse(_accesor?.HttpContext?.User?.Identity?.Name);
    }
}
