using System;
using System.ComponentModel.DataAnnotations;

namespace IMDb.Application.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O {0} field is required")]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
