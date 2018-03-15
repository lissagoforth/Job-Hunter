using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class CompanyAddNoteViewModel
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Company's Name")]
        public string CompanyName { get; set; }

        public string Note { get; set; }
    }
}
