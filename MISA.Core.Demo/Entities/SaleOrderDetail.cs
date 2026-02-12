using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Entities
{
    [Table("sale_order_detail")]
    public class SaleOrderDetail
    {
        [Key]
        public int SaleOrderDetailId { get; set; }
        public int SaleOrderId { get; set; }
        [StringLength(100)]
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public Decimal ? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
    }
}
