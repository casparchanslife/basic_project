using System;
using System.ComponentModel.DataAnnotations;


namespace Learn.ViewModel
{
    public class ViewModelBase
    {
        [Display(Name = "狀態")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Display(Name = "建立時間")]
        public Nullable<DateTime> CreateDate { get; set; }

        //[Display(Name = "建立用戶")]
        //[MaxLength(50)]
        //public string CreatedBy { get; set; }

        [Display(Name = "最後修改時間")]
        public Nullable<DateTime> UpdateDate { get; set; }

        //[Display(Name = "最後修改用戶")]
        //[MaxLength(50)]
        //public string UpdatedBy { get; set; }
    }
}
