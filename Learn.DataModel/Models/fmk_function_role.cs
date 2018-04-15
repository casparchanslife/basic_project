using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.DataModel.Models
{
    public partial class fmk_function_role
    {
        [Key, Column(Order = 0)]
        public string function_id { get; set; }
        [Key, Column(Order = 1)]
        public string role_id { get; set; }
        public bool can_update { get; set; }
        public bool can_insert { get; set; }
        public bool can_delete { get; set; }
        public bool can_read { get; set; }
        public bool can_copy { get; set; }

        [ForeignKey("role_id")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        [ForeignKey("function_id")]
        public virtual fmk_function fmk_function { get; set; }
    }
}
