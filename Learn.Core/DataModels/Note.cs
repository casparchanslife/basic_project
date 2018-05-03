using Learn.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.Core.DataModels
{
    [Table("Note")]
    public partial class Note : ModelBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Message")]
        [StringLength(30)]
        public string Message { get; set; }
    }
}
