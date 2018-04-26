using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Learn.Web.Models.ViewModels;
using Learn.DataModel.Models;
using Learn.Web.Core.Manager;

namespace Learn.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}