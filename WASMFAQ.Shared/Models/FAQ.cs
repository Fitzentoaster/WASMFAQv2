using System.ComponentModel.DataAnnotations;

namespace WASMFAQv2.Shared.Models
{
    public class FAQ
    {
        [Key]
        public int FAQId { get; set; }
        public String? Title { get; set; } = "FAQ";
        public String? Description { get; set; } = "Description";

    }
}
