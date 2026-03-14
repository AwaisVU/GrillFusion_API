using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrillFusion_API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string PickUpName { get; set; } = string.Empty;
        [Required]
        public string PickUpPhoneNo { get; set; } = string.Empty;
        [Required]
        public string PickUpEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;

        //Foreign Key of the application user who places the order
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public double OrderTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalItem {  get; set; }

        //One order can have many Order Details so make a list of OrderDetails
        public List<OrderDetail> OrderDetails { get; set; } = new();

    }
}
