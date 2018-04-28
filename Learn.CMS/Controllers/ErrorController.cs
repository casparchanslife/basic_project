using Learn.lib.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.CMS.Controllers
{
    public class ErrorController : BaseController
    {
        public ViewResult UnAuthorized()
        {
            return View();
        }
    }
}
