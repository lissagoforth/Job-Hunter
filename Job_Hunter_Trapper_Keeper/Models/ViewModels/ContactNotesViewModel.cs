using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class ContactNotesViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Contact's Name")]
        public string ContactName { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Notes")]
        public virtual IEnumerable<ContactNotes> ContactNotes { get; set; }
    }
}
