using Learn.lib.Attributes;
using Learn.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ViewResult UnAuthorized()
        {
            return View();
        }
    }
}
