using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Learn.DataModel.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public  class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Status")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedById { get; set; }

        public virtual ApplicationUser UpdatedBy { get; set; }

        public ApplicationUser()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Status = "A";
        }

    }

}