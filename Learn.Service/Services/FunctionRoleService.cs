using System.Collections.Generic;
using Learn.DataModel.Models;
using Learn.Lib.Infrastructure.Interface;
using System.Threading.Tasks;
using Learn.Data.Repositories;
using Learn.Service.Manager;
using Learn.ViewModel;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using System;
using Learn.Lib.Infrastructure.Repositories;

namespace Learn.Service
{
    #region Interface
    public interface IFunctionRoleService
    {
        List<UserRoleAccessRightViewModel> GetUserRoleAccessRight(string roleId);
        void SaveUser();
    }

    #endregion

    public class FunctionRoleService : IFunctionRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFunctionRoleRepository functionRoleRepository;


        public FunctionRoleService(IFunctionRoleRepository functionRoleRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManger, ApplicationRoleManager roleManager)
        {
            this.functionRoleRepository = functionRoleRepository;
            this.unitOfWork = unitOfWork;
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
