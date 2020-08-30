using System;

namespace IMDb.Application.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Guid RoleId { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
