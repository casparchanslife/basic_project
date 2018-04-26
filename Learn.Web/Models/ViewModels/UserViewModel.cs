using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Learn.lib.Models.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Email")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Role")]
        public List<string> RoleId { get; set; }

        public List<SelectListItem> RoleOptions { get; set; }
    }

}
