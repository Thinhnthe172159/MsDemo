using Dapper;
using MISA.Core.Demo;
using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Interfaces.IRepositories;
using System.Data;

namespace MISA.Demo.Infrastucture.Repositories
{
    /// <summary>
    /// Triển khai các thao tác dữ liệu khách hàng sử dụng Dapper
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _context;

        public CustomerRepository(IDbConnection context)
        {
            _context = context;
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customersDTO">Thông tin khách hàng cần thêm</param>
        /// <returns>Kết quả thêm mới</returns>
        public async Task<bool> AddCustomer(CustomersDTO customersDTO)
        {
            var rowsAffected = await _context.ExecuteAsync(
                "proc_add_customer",
                new
                {
                    p_customer_name = customersDTO.p_customer_name,
                    p_customer_code = customersDTO.p_customer_code,
                    p_customer_address = customersDTO.p_customer_address
                },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected == 1;
        }

        /// <summary>
        /// Xóa khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Kết quả xóa</returns>
        public Task<bool> DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Thông tin khách hàng, null nếu không tồn tại</returns>
        public async Task<Customer?> GetCustomer(Guid customerId)
        {
            return await _context.QuerySingleOrDefaultAsync<Customer>(
                "proc_get_customer_by_id",
                new { p_customer_id = customerId },
                commandType: CommandType.StoredProcedure
            );
        }

        /// <summary>
        /// Lấy danh sách khách hàng theo điều kiện tìm kiếm
        /// </summary>
        /// <param name="customersDTO">Thông tin điều kiện tìm kiếm</param>
        /// <returns>Danh sách khách hàng</returns>
        public async Task<List<Customer>> GetCustomers(CustomersDTO customersDTO)
        {
            var result = await _context.QueryAsync<Customer>(
                "proc_search_customer",
                new
                {
                    p_customer_name = customersDTO.p_customer_name,
                    p_customer_code = customersDTO.p_customer_code,
                    p_customer_address = customersDTO.p_customer_address
                },
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        public async Task<bool> UpdateCustomer(UpdateCustomerDTO customer)
        {
            var rowsAffected = await _context.ExecuteAsync(
                "proc_update_customer",
                new
                {
                    p_customer_id = customer.customer_id,
                    p_customer_name = customer.customer_name,
                    p_customer_address = customer.customer_address
                },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected == 1;
        }
    }
}
