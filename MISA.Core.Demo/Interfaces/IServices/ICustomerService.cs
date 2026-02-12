using MISA.Core.Demo.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Interfaces.IServices
{
    /// <summary>
    /// Interface định nghĩa các nghiệp vụ liên quan đến khách hàng
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Lấy danh sách khách hàng theo điều kiện tìm kiếm
        /// </summary>
        /// <param name="customersDTO">Thông tin tìm kiếm khách hàng</param>
        /// <returns>Danh sách khách hàng</returns>
        Task<ServiceResult<List<Customer>>> GetCustomers(CustomersDTO customersDTO);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customersDTO">Thông tin khách hàng cần thêm</param>
        /// <returns>Kết quả thêm mới</returns>
        Task<ServiceResult<bool>> Insert(CustomersDTO customersDTO);

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="updateCustomerDTO">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        Task<ServiceResult<bool>> Update(UpdateCustomerDTO updateCustomerDTO);

        /// <summary>
        /// Xóa khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Kết quả xóa</returns>
        Task<ServiceResult<bool>> Delete(Guid customerId);

        /// <summary>
        /// Lấy thông tin khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Thông tin khách hàng</returns>
        Task<ServiceResult<Customer>> GetById(Guid customerId);
    }
}
