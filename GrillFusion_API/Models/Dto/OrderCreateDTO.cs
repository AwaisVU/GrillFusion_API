using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrillFusion_API.Models.Dto
{
    public class OrderCreateDTO
    {
       
        [Required]
        public string PickUpName { get; set; } = string.Empty;
        [Required]
        public string PickUpPhoneNo { get; set; } = string.Empty;
        [Required]
        public string PickUpEmail { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; } = string.Empty;
        public double OrderTotal { get; set; }
        public int TotalItem { get; set; }

        //One order can have many Order Details so make a list of OrderDetails
        public List<OrderDetailCreateDTO> OrderDetailDTO { get; set; } = new();
    }
}
