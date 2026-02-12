using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MISA.Core.Demo
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public string? customerCode { get; set; }
        public string? customerName { get; set; }
        public string? customerAddress { get; set; }
    }
}
