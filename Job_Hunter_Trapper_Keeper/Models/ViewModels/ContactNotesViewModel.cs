using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class ContactNotesViewModel
    {
        [Required]
        [Display(Name = "Contact's Name")]
        public string ContactName { get; set; }

        [Display(Name = "Notes")]
        public virtual IEnumerable<ContactNotes> ContactNotes { get; set; }
    }
}
