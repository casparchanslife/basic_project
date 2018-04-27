using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.DataModel
{
    public partial class fmk_function
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string function_id { get; set; }
        public int function_group_id { get; set; }
        public string url_area { get; set; }
        public string url_controller { get; set; }
        public string url_function { get; set; }
        public string description { get; set; }
        public int iorder { get; set; }

        [ForeignKey("function_group_id")]
        public virtual fmk_function_group fmk_function_group { get; set; }
        public virtual ICollection<fmk_function_role> fmk_function_roles { get; set; }
    }
}
