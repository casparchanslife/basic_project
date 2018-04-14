using Learn.lib.Attributes;
using Learn.lib.DataEnum;
using Learn.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Web.Controllers
{
    [CustomAuthorize(Roles="Admin")]
    public class RolesController : BaseController
    {
        public async Task<ActionResult> GetRole(string Id)
        {
            var role = await this.RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
            }
            return HttpNotFound();
        }

        public ActionResult Index()
        {
            var roles = this.RoleManager.Roles;
            var roleList = new List<RoleBindingModel> ();
            foreach(var role in roles)
            {
                roleList.Add(new RoleBindingModel(){ Id = role.Id, Name = role.Name});
            }
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
            var role = new IdentityRole { Name = model.Name };
            var result = await this.RoleManager.CreateAsync(role);
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
