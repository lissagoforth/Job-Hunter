using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class ContactAddNoteViewModel
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Contact's Name")]
        public string ContactName { get; set; }

        public string Note { get; set; }
    }
}
