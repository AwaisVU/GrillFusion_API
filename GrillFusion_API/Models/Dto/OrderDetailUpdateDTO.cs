using System.ComponentModel.DataAnnotations;

namespace GrillFusion_API.Models.Dto
{
    public class OrderDetailUpdateDTO
    {
        [Required]
        public int OrderDetailId { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
    }
}
