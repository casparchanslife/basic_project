using Learn.DataModel.Models;
using Learn.lib.Attributes;
using Learn.Web.Core.Manager;
using Learn.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;

namespace Learn.Web.Controllers
{
    public class RolesController : BaseController
    {
        private readonly ApplicationRoleManager RoleManager;

        public RolesController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public async Task<ActionResult> GetRole(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
            }
            return HttpNotFound();
        }

        public ActionResult Index()
        {
            var roleList = RoleManager.Roles.ToList();
            var roleViewModel = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleBindingModel>>(roleList);
            return View(roleViewModel);
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
            var role = new ApplicationRole { Name = model.Name };
            var result = await RoleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string Id)
        {
            var role = await this.RoleManager.FindByIdAsync(Id);
            if (role != null)
            {
                return View(new RoleBindingModel() { Name = role.Name });
            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var role = await this.RoleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                var result = await this.RoleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    if (result.Errors != null)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> Delete(string Id)
        {
            var role = await this.RoleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var result = await this.RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    if (result.Errors != null)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                    //
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

    }
}
