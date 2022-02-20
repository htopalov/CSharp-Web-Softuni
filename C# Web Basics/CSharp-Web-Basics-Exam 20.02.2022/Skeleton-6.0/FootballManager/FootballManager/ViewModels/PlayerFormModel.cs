using System.ComponentModel.DataAnnotations;

namespace FootballManager.ViewModels
{
    public class PlayerFormModel
    {
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Position { get; set; }

        [Required]
        [Range(0, 10)]
        public byte Speed { get; set; }

        [Required]
        [Range(0, 10)]
        public byte Endurance { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
