using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company company;

        public virtual ICollection<JobNotes> jobNotes { get; set; }
        public JobNotes JobNotes;

    }
}
