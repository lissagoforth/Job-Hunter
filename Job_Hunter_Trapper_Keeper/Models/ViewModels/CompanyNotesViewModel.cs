using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class CompanyNotesViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Company's Name")]
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Notes")]
        public virtual IEnumerable<CompanyNotes> CompanyNotes { get; set; }
    }
}
