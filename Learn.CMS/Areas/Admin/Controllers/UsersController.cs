using Learn.Core.Services;
using Learn.Core.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.CMS.Areas.Admin.Controllers
{
    [RequireHttps]
    public class UsersController : Controller
    {
        public readonly IUserService ApplicationUserService;

        public UsersController(IUserService applicationUserService)
        {
            this.ApplicationUserService = applicationUserService;
        }


        public ActionResult Index()
        {
            var userViewModelList = ApplicationUserService.GetUsers();
            return View(userViewModelList);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var userViewModel = await ApplicationUserService.GetUser(id);
            if(userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult>  Edit(UserViewModel model)
        {
            var user = await ApplicationUserService.GetUser(model.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            await ApplicationUserService.EditUserAsync(model);

            return RedirectToAction("Index","Users");

        }
    }
}