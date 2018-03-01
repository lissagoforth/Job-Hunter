using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public ICollection<CompanyNotes> companyNotes { get; set; }
        public CompanyNotes CompanyNotes;
    }
}
