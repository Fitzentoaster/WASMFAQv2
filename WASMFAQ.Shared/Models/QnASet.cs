using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WASMFAQv2.Shared.Models
{
    public class QnASet
    {
        [Key]
        public int QnASetId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public List<QnA> QnAs { get; set; } = new List<QnA>();
    }


}
