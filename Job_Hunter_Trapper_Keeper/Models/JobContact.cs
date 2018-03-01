using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models
{
    public class JobContact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }
        public Job job;

        [Required]
        public int ContactId { get; set; }
        public Contact contact;

    }
}
