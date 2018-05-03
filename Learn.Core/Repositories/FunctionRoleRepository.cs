using Learn.Core.Infrastructure.Interface;
using Learn.Core.DataModels;
using System.Collections.Generic;
using System.Linq;
using Learn.Core.ViewModels;

namespace Learn.Core.Infrastructure.Repositories
{

    #region Interface
    public interface IFunctionRoleRepository : IRepository<fmk_function>
    {
        List<fmk_function> GetAccessRightList();
        void UpdateAccessRight(UserRoleAccessRightViewModel list);
    }
    #endregion

    public class FunctionRoleRepository : RepositoryBase<fmk_function>, IFunctionRoleRepository
    {
        public FunctionRoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<fmk_function> GetAccessRightList()
        {
            return DataContext.fmk_function.ToList();
        }

        public void UpdateAccessRight(UserRoleAccessRightViewModel model)
        {
            var accessRight = DataContext.fmk_function_role.FirstOrDefault(o => o.role_id == model.role_id && o.function_id == model.function_id);
            if (accessRight != null)
            {
                accessRight.can_delete = model.can_delete;
                accessRight.can_insert = model.can_insert;
                accessRight.can_read = model.can_read;
                accessRight.can_update = model.can_update;
            }
            else
            {
                accessRight = new fmk_function_role();
                accessRight.function_id = model.function_id;
                accessRight.can_delete = model.can_delete;
                accessRight.can_insert = model.can_insert;
                accessRight.can_read = model.can_read;
                accessRight.can_update = model.can_update;
                accessRight.role_id = model.role_id;
                DataContext.fmk_function_role.Add(accessRight);
            }
        }
    }
}
