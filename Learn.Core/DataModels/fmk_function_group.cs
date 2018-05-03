using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.Core.DataModels
{
    public partial class fmk_function_group
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int function_group_id { get; set; }
        public int iorder { get; set; }
        public string locale { get; set; }
        public string description { get; set; }

        public virtual ICollection<fmk_function> fmk_functions { get; set; }
    }
}
