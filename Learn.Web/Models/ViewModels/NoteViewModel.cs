using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.lib.Models.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        [Key]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0}必須填寫")]
        [Display(Name = "Message")]
        [StringLength(30)]
        public string Message { get; set; }

    }
}
