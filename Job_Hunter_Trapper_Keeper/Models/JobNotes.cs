using System.ComponentModel.DataAnnotations;

namespace Job_Hunter_Trapper_Keeper.Models
{
    public class JobNotes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser User;

        [Required]
        public int JobId { get; set; }
        public Job Job { get; set; }

        [Required]
        public string Notes { get; set; }
    }
}