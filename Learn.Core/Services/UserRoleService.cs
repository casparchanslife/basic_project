using System.Collections.Generic;
using Learn.Core.DataModels;
using Learn.Core.Infrastructure.Interface;
using System.Threading.Tasks;
using Learn.Core.Repositories;
using Learn.Core.Manager;
using AutoMapper;
using System.Linq;
using System;
using Learn.Core.Infrastructure.Repositories;
using Learn.Core.ViewModels;

namespace Learn.Core.Services
{
    #region Interface
    public interface IUserRoleService
    {
        Task<RoleBindingModel> GetUserRole(string userId);
        void SaveUserRole();
        IEnumerable<RoleBindingModel> GetUserRoles();
        Task EditUserRoleAsync(RoleBindingModel model);
        Task CreateUserRoleAsync(RoleBindingModel model);
        Task DeleteUserRoleAsync(RoleBindingModel model);

    }

    #endregion

    public class UserRoleService : IUserRoleService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IFunctionRoleRepository functionRoleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationUserManager UserManager;
        private readonly ApplicationRoleManager RoleManager;

        public UserRoleService(IApplicationUserRepository applicationUserRepository, IFunctionRoleRepository functionRoleRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManger, ApplicationRoleManager roleManager)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.functionRoleRepository = functionRoleRepository;
            this.unitOfWork = unitOfWork;
            this.UserManager = userManger;
            this.RoleManager = roleManager;
        }

        public IEnumerable<RoleBindingModel> GetUserRoles()
        {
            var roleList = RoleManager.Roles.ToList();
            var roleViewModelList = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleBindingModel>>(roleList);
            return roleViewModelList;
        }

        #region IUserService Members

        public async Task<RoleBindingModel> GetUserRole(string userId)
        {
            var role = await RoleManager.FindByIdAsync(userId);
            var roleViewModel = Mapper.Map<ApplicationRole, RoleBindingModel>(role);
            return roleViewModel;
        }

        public async Task EditUserRoleAsync(RoleBindingModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            var result = await this.RoleManager.UpdateAsync(role);

            for (int i = 0; i < model.AccessRightList.Count(); i++)
            {
                functionRoleRepository.UpdateAccessRight(model.AccessRightList[i]);
            }
            SaveUserRole();
        }

        public async Task CreateUserRoleAsync(RoleBindingModel model)
        {
            var role = Mapper.Map<RoleBindingModel, ApplicationRole>(model);
            role.Id = new Guid().ToString();
            var result = await this.RoleManager.CreateAsync(role);
        }


        public async Task DeleteUserRoleAsync(RoleBindingModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            await RoleManager.DeleteAsync(role);
        }

        public void SaveUserRole()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
