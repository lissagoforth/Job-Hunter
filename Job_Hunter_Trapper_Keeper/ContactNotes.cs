using System.ComponentModel.DataAnnotations;

namespace Job_Hunter_Trapper_Keeper.Models
{
    public class ContactNotes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser User;
    }
}