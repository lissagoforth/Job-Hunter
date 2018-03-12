using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class CompanyNotesViewModel
    {
        [Required]
        [Display(Name = "Company's Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Notes")]
        public virtual IEnumerable<CompanyNotes> CompanyNotes { get; set; }
    }
}
