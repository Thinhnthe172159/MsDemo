using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Dto
{
    public class UpdateCustomerDTO
    {
        [Required]
        public Guid customer_id { get; set; } = Guid.Empty;
        public string? customer_name { get; set; }
        public string? customer_address { get; set; }
    }
}
