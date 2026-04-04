using System.ComponentModel.DataAnnotations;

namespace GrillFusion_API.Models.Dto
{
    public class ForgotPasswordDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        
    }
}
