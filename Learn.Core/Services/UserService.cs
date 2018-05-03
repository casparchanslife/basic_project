using System.Collections.Generic;
using Learn.Core.DataModels;
using Learn.Core.Infrastructure.Interface;
using System.Threading.Tasks;
using Learn.Core.Repositories;
using Learn.Core.Manager;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using Learn.Core.ViewModels;

namespace Learn.Core.Services
{
    #region Interface
    public interface IUserService
    {
        Task<UserViewModel> GetUser(string userId);
        void SaveUser();
        IEnumerable<UserViewModel> GetUsers();
        Task EditUserAsync(UserViewModel user);

    }

    #endregion

    public class UserService : IUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationUserManager UserManager;
        private readonly ApplicationRoleManager RoleManager;

        public UserService(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManger, ApplicationRoleManager roleManager)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.unitOfWork = unitOfWork;
            this.UserManager = userManger;
            this.RoleManager = roleManager;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var userList = UserManager.Users.ToList();
            var userViewModelList = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(userList);
            return userViewModelList;
        }


        #region IUserService Members

        public async Task<UserViewModel> GetUser(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var userViewModel = Mapper.Map<ApplicationUser, UserViewModel>(user);
            var userRoleNames = (List<string>)await UserManager.GetRolesAsync(user.Id);
            var roleOptions = RoleManager.Roles.Select(o => new SelectListItem() { Value = o.Id, Text = o.Name, Selected = true }).ToList();
            userViewModel.RoleId = roleOptions.Where(o => userRoleNames.Any(oo => oo == o.Text)).Select(o => o.Value).ToList();
            userViewModel.RoleOptions = new List<SelectListItem>(roleOptions);
            return userViewModel;
        }


        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public async Task EditUserAsync(UserViewModel model)
        {
            var user = Mapper.Map<UserViewModel, ApplicationUser>(model);

            var updateUserResult = await UserManager.UpdateAsync(user);

            var currentRoles = await UserManager.GetRolesAsync(user.Id.ToString());

            var rolesNotExists = model.RoleId.Except(RoleManager.Roles.Select(x => x.Id)).ToArray();

            var removeResult = await UserManager.RemoveFromRolesAsync(user.Id.ToString(), currentRoles.ToArray());

            var addResult = await UserManager.AddToRolesAsync(user.Id.ToString(), RoleManager.Roles.Where(o => model.RoleId.Contains(o.Id)).Select(o => o.Name).ToArray());
        }

        #endregion
    }
}
