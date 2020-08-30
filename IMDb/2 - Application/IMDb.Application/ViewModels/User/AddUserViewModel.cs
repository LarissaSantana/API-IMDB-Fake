using System;

namespace IMDb.Application.ViewModels.User
{
    public class AddUserViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public bool Status { get; set; }
    }
}
