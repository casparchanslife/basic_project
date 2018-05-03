using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Learn.Core.DataModels
{
    public  class ApplicationRole : IdentityRole
    {
        [StringLength(30)]
        public string Description { get; set; }

        public virtual ICollection<fmk_function_role> fmk_function_roles { get; set; }
    }

}