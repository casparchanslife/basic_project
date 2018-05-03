using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Learn.Core.DataModels;

namespace Learn.CMS.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}