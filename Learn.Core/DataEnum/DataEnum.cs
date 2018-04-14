using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.lib.DataEnum
{
    public class DataEnum
    {
        public enum RolePermission
        {
            Admin,
            Writter
        }

        public enum PermissionType
        {
            Create,
            Edit,
            Delete,
            Read
        }
    }
}
