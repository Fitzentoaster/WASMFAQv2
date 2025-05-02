using System.ComponentModel.DataAnnotations;

namespace WASMFAQv2.Shared.Models
{
    public class QnA
    {
        [Key]
        public int QnAId { get; set; }
        public int? QnASetId { get; set; }
        public string? Question { get; set; } = "Question";
        public string? Answer { get; set; } = "Answer";

    }
}
