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
        public List<QnA>? QnAs { get; set; }
    }


}
