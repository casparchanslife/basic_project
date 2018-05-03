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
