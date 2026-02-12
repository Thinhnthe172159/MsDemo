using Microsoft.AspNetCore.Mvc;
using MISA.Core.Demo;
using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Helpers;
using MISA.Core.Demo.Interfaces.IServices;

namespace MISA.Demo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICustomerService customerService,
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Tìm kiếm khách hàng và trả về danh sách
        /// </summary>
        /// <param name="customersDTO">Điều kiện tìm kiếm</param>
        /// <returns>Danh sách khách hàng</returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomersDTO customersDTO)
        {
            var res = await _customerService.GetCustomers(customersDTO);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo Id
        /// </summary>
        /// <param name="id">Id khách hàng</param>
        /// <returns>Thông tin khách hàng</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var res = await _customerService.GetById(id);
            return res.Success ? Ok(res) : NotFound(res);
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customerDTO">Thông tin khách hàng</param>
        /// <returns>Kết quả thêm mới</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomersDTO customerDTO)
        {
            var res = await _customerService.Insert(customerDTO);
            return res.Success ? Created(string.Empty, res) : BadRequest(res);
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO customer)
        {
            var res = await _customerService.Update(customer);
            return res.Success ? Ok(res) : BadRequest(res);
        }
    }
}
