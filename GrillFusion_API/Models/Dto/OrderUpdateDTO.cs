using System.ComponentModel.DataAnnotations;

namespace GrillFusion_API.Models.Dto
{
    public class OrderUpdateDTO
    {
        [Required]
        public int OrderId { get; set; }
        public string PickUpName { get; set; } = string.Empty;
        public string PickUpPhoneNo { get; set; } = string.Empty;
        public string PickUpEmail { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
    }
}
