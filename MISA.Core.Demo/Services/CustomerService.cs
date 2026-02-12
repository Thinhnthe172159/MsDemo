using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Interfaces.IRepositories;
using MISA.Core.Demo.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Services
{
    /// <summary>
    /// Xử lý nghiệp vụ liên quan đến khách hàng
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Xóa khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Kết quả xóa</returns>
        public Task<ServiceResult<bool>> Delete(Guid customerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo Id
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Thông tin khách hàng</returns>
        public async Task<ServiceResult<Customer>> GetById(Guid customerId)
        {
            var res = new ServiceResult<Customer>();
            var customer = await _customerRepository.GetCustomer(customerId);

            if (customer == null)
            {
                res.Success = false;
                res.Message = "Not found customer";
                return res;
            }

            res.Data = customer;
            return res;
        }

        /// <summary>
        /// Lấy danh sách khách hàng theo điều kiện tìm kiếm
        /// </summary>
        /// <param name="customersDTO">Điều kiện tìm kiếm</param>
        /// <returns>Danh sách khách hàng</returns>
        public async Task<ServiceResult<List<Customer>>> GetCustomers(CustomersDTO customersDTO)
        {
            var res = new ServiceResult<List<Customer>>();
            var result = await _customerRepository.GetCustomers(customersDTO);

            if (!result.Any())
            {
                res.Success = false;
                res.Message = "Not match any result!";
                res.Data = result;
                return res;
            }

            res.Data = result;
            return res;
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customersDTO">Thông tin khách hàng</param>
        /// <returns>Kết quả thêm mới</returns>
        public async Task<ServiceResult<bool>> Insert(CustomersDTO customersDTO)
        {
            var res = new ServiceResult<bool>();

            if (string.IsNullOrWhiteSpace(customersDTO.p_customer_code))
            {
                res.Success = false;
                res.Message = "Customer code is required";
                return res;
            }

            if (string.IsNullOrWhiteSpace(customersDTO.p_customer_name))
            {
                res.Success = false;
                res.Message = "Customer name is required";
                return res;
            }

            if (string.IsNullOrWhiteSpace(customersDTO.p_customer_address))
            {
                res.Success = false;
                res.Message = "Customer address is required";
                return res;
            }

            try
            {
                var isSuccess = await _customerRepository.AddCustomer(customersDTO);
                res.Data = isSuccess;
                res.Success = isSuccess;
                res.Message = isSuccess ? "Add customer success!" : "Add customer failed!";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="updateCustomerDTO">Thông tin khách hàng cần cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        public async Task<ServiceResult<bool>> Update(UpdateCustomerDTO updateCustomerDTO)
        {
            var res = new ServiceResult<bool>();

            var customer = await _customerRepository.GetCustomer(updateCustomerDTO.customer_id);
            if (customer == null)
            {
                res.Success = false;
                res.Message = "Not found customer";
                return res;
            }

            if (string.IsNullOrWhiteSpace(updateCustomerDTO.customer_name))
            {
                res.Success = false;
                res.Message = "Customer name is required";
                return res;
            }

            if (string.IsNullOrWhiteSpace(updateCustomerDTO.customer_address))
            {
                res.Success = false;
                res.Message = "Customer address is required";
                return res;
            }

            try
            {
                var isSuccess = await _customerRepository.UpdateCustomer(updateCustomerDTO);
                res.Data = isSuccess;
                res.Success = isSuccess;
                res.Message = isSuccess ? "Update customer success!" : "Update customer failed!";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
