using Learn.DataModel.Models;
using Learn.lib.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using Learn.Service.Manager;
using Learn.ViewModel;
using Learn.CMS.Controllers;
using Learn.Service;

namespace Learn.CMS.Areas.Admin.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IUserRoleService UserRoleService;

        public RolesController(IUserRoleService userRoleService)
        {
            this.UserRoleService = userRoleService;
        }

        public ActionResult Index()
        {
            var roleList = UserRoleService.GetUserRoles();
            return View(roleList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new RoleBindingModel());
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
                await UserRoleService.CreateUserRoleAsync(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string Id)
        {
            var roleViewModel = await UserRoleService.GetUserRole(Id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleBindingModel model)
        {
            var roleViewModel = await UserRoleService.GetUserRole(model.Id);
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
                    await UserRoleService.EditUserRoleAsync(model);
                }
            }
            return RedirectToAction("Index", "Roles");
        }

        public async Task<ActionResult> Delete(string Id)
        {
            var roleViewModel = await UserRoleService.GetUserRole(Id);

            if (roleViewModel == null)
            {
                return HttpNotFound();
            }else
            {
                await UserRoleService.DeleteUserRoleAsync(roleViewModel);
            }
            return HttpNotFound();
        }

    }
}
