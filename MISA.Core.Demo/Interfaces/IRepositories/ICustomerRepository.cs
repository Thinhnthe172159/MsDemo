using MISA.Core.Demo.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Interfaces.IRepositories
{
    /// <summary>
    /// Interface định nghĩa các thao tác dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy danh sách khách hàng theo điều kiện tìm kiếm
        /// </summary>
        /// <param name="getCustomersDTO">Thông tin điều kiện tìm kiếm</param>
        /// <returns>Danh sách khách hàng</returns>
        Task<List<Customer>> GetCustomers(CustomersDTO getCustomersDTO);

        /// <summary>
        /// Lấy thông tin khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Thông tin khách hàng, null nếu không tồn tại</returns>
        Task<Customer?> GetCustomer(Guid customerId);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customersDTO">Thông tin khách hàng cần thêm</param>
        /// <returns>Kết quả thêm mới</returns>
        Task<bool> AddCustomer(CustomersDTO customersDTO);

        /// <summary>
        /// Xóa khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Kết quả xóa</returns>
        Task<bool> DeleteCustomer(Guid customerId);

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        Task<bool> UpdateCustomer(UpdateCustomerDTO customer);
    }
}
