using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MISA.Core.Demo.Entities
{
    /// <summary>
    /// Đơn bán hàng
    /// </summary>
    [Table("sale_order")]
    public class SaleOrder
    {
        /// <summary>
        /// Id đơn bán (tự tăng)
        /// </summary>
        [Key]
        public int SaleOrderId { get; set; }

        /// <summary>
        /// Id khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Số đơn bán
        /// </summary>
        public string? SaleOrderNo { get; set; }

        /// <summary>
        /// Ngày tạo đơn
        /// </summary>
        public DateTime? SaleOrderDate { get; set; }

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal? TotalAmount { get; set; }
    }
}
