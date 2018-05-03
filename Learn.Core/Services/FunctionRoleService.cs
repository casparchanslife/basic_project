using System.Collections.Generic;
using Learn.Core.Infrastructure.Interface;
using Learn.Core.Manager;
using System.Linq;
using Learn.Core.Infrastructure.Repositories;
using Learn.Core.ViewModels;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Learn.Core.Services
{
    #region Interface
    public interface IFunctionRoleService
    {
        List<UserRoleAccessRightViewModel> GetUserRoleAccessRight(string roleId);
        List<UserRoleAccessRightViewModel> GetAuthenticatedUserRolesAccessRight();
        void SaveUser();
    }

    #endregion

    public class FunctionRoleService : IFunctionRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFunctionRoleRepository functionRoleRepository;
        private readonly ApplicationRoleManager roleManger;
        private readonly ApplicationUserManager userManger;


        public FunctionRoleService(IFunctionRoleRepository functionRoleRepository, IUnitOfWork unitOfWork, ApplicationRoleManager roleManger, ApplicationUserManager userManger)
        {
            this.functionRoleRepository = functionRoleRepository;
            this.unitOfWork = unitOfWork;
            this.roleManger = roleManger;
            this.userManger = userManger;
        }

        public List<UserRoleAccessRightViewModel> GetAuthenticatedUserRolesAccessRight()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var roles = userManger.GetRoles(userId);
            var rolesId = roleManger.Roles.Where(o => roles.Contains(o.Name)).Select(o=>o.Id).ToList();
            var accessRightList = functionRoleRepository.GetAccessRightList().SelectMany(o => o.fmk_function_roles).Where(o=> rolesId.Contains(o.role_id)).ToList();
            var modelList = accessRightList.Select(o=>
                             new UserRoleAccessRightViewModel()
                             {
                                 role_id = o.role_id,
                                 function_id = o.function_id,
                                 function_description = o.fmk_function.description,
                                 function_group_id = o.fmk_function.fmk_function_group.function_group_id,
                                 description = o.fmk_function.fmk_function_group.description,
                                 can_read = o.can_read,
                                 can_delete = o.can_delete,
                                 can_update =  o.can_update,
                                 can_insert = o.can_insert,
                             }).ToList();
            return modelList;
        }

        public List<UserRoleAccessRightViewModel> GetUserRoleAccessRight(string roleId)
        {
            var accessRightList = functionRoleRepository.GetAccessRightList();

            var modelList = (from arl in accessRightList
                             let role = arl.fmk_function_roles.FirstOrDefault(oo => oo.role_id == roleId)
                             select new UserRoleAccessRightViewModel()
                             {
                                 role_id = roleId,
                                 function_id = arl.function_id,
                                 function_description = arl.description,
                                 function_group_id = arl.fmk_function_group.function_group_id,
                                 description = arl.fmk_function_group.description,
                                 can_read = role != null ? role.can_read : false,
                                 can_delete = role != null ? role.can_delete : false,
                                 can_update = role != null ? role.can_update : false,
                                 can_insert = role != null ? role.can_insert : false,
                             }).ToList();
            return modelList;
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

    }
}
