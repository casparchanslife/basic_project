using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.DataEnum
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

    public class AccessLevelType
    {
        public const string Read = "Read";
        public const string Insert = "Insert";
        public const string Delete = "Delete";
        public const string Update = "Update";
    }

    public class FunctionIDs
    {
        public const string NotesgMgt = "NM01";
    }

}