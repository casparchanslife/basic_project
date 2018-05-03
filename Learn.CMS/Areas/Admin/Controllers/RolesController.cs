using System.Threading.Tasks;
using System.Web.Mvc;
using Learn.CMS.Controllers;
using Learn.Core.ViewModels;
using Learn.Core.Services;
using System.Collections.Generic;

namespace Learn.CMS.Areas.Admin.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IUserRoleService userRoleService;
        private readonly IFunctionRoleService functionRoleService;

        public RolesController(IUserRoleService userRoleService, IFunctionRoleService functionRoleService)
        {
            this.userRoleService = userRoleService;
            this.functionRoleService = functionRoleService;
        }

        public ActionResult Index()
        {
            var roleList = userRoleService.GetUserRoles();
            return View(roleList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RoleBindingModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await userRoleService.CreateUserRoleAsync(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string Id)
        {
            var roleViewModel = await userRoleService.GetUserRole(Id);
            roleViewModel.AccessRightList = functionRoleService.GetUserRoleAccessRight(roleViewModel.Id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleBindingModel model)
        {
            var roleViewModel = await userRoleService.GetUserRole(model.Id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    await userRoleService.EditUserRoleAsync(model);
                }
            }
            return RedirectToAction("Index", "Roles");
        }

        public async Task<ActionResult> Delete(string Id)
        {
            var roleViewModel = await userRoleService.GetUserRole(Id);

            if (roleViewModel == null)
            {
                return HttpNotFound();
            }else
            {
                await userRoleService.DeleteUserRoleAsync(roleViewModel);
            }
            return RedirectToAction("Index", "Roles");
        }

    }
}
