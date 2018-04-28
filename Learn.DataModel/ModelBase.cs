using Learn.DataModel.Models;
using Learn.Lib;
using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.DataModel
{
    public class ModelBase
    {
        [Display(Name = "Status")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Created By")]
        public virtual ApplicationUser CreatedBy { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Updated By")]
        public virtual ApplicationUser UpdatedBy { get; set; }


    }
}
