using AutoMapper;
using Learn.Lib;
using Learn.Lib.Extensions;
using Learn.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Learn.Service.Manager;
using Learn.ViewModel;

namespace Learn.Web.Controllers
{
    [RequireHttps]
    public class UsersController : Controller
    {
        public readonly ApplicationUserManager UserManager;
        private readonly ApplicationRoleManager RoleManager;

        public UsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }


        public ActionResult Index()
        {
            var userList = UserManager.Users.ToList();
            var userViewModelList = Mapper.Map<IEnumerable<ApplicationUser>,IEnumerable <UserViewModel>>(userList);
            return View(userViewModelList);
        }



        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var userViewModel = new UserViewModel();
            userViewModel.Status = user.Status;
            userViewModel.UpdateDate = user.UpdateDate;
            userViewModel.CreateDate = user.CreateDate;
            userViewModel.Id = user.Id;
            userViewModel.Email = user.Email;
            var userRoleNames = (List<string>) await UserManager.GetRolesAsync(user.Id);
            var roleOptions = RoleManager.Roles.Select(o=> new SelectListItem () { Value = o.Id, Text = o.Name, Selected = true }).ToList();
            userViewModel.RoleId = roleOptions.Where(o => userRoleNames.Any(oo => oo == o.Text)).Select(o => o.Value).ToList();
            userViewModel.RoleOptions = new List<SelectListItem>(roleOptions);
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult>  Edit(UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            user.Email = model.Email;
            var updateUserResult = await UserManager.UpdateAsync(user);

            var currentRoles = await UserManager.GetRolesAsync(user.Id.ToString());

            var rolesNotExists = model.RoleId.Except(RoleManager.Roles.Select(x => x.Id)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return View(model);
            }

            var removeResult = await UserManager.RemoveFromRolesAsync(user.Id.ToString(), currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return View(model);
            }

            var addResult = await UserManager.AddToRolesAsync(user.Id.ToString(), RoleManager.Roles.Where(o => model.RoleId.Contains(o.Id)).Select(o=>o.Name).ToArray());

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return View(model);
            }

            return RedirectToAction("Index","Users");

        }
    }
}