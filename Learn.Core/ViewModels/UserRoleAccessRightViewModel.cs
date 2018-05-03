using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.ViewModels
{
    public class UserRoleAccessRightViewModel
    {
        public string role_id { get; set; }
        public string function_id { get; set; }
        public string function_description { get; set; }
        public int function_group_id { get; set; }
        public string description { get; set; }
        public bool can_read { get; set; }
        public bool can_insert { get; set; }
        public bool can_update { get; set; }
        public bool can_delete { get; set; }
    }
}
