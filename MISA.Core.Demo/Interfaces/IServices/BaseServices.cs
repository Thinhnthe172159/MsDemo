using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Helpers;
using MISA.Core.Demo.Interfaces.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Interfaces.IServices
{
    /// <summary>
    /// Lớp service cơ sở, triển khai các nghiệp vụ CRUD dùng chung
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng nghiệp vụ</typeparam>
    public class BaseServices<T> : IBaseServices<T>
    {
        /// <summary>
        /// Repository cơ sở thao tác với dữ liệu
        /// </summary>
        private readonly IBaseRepository<T> baseRepository;

        /// <summary>
        /// Khởi tạo service với repository tương ứng
        /// </summary>
        /// <param name="baseRepository">Repository cơ sở</param>
        public BaseServices(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="data">Đối tượng cần thêm</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        public async Task<ServiceResult<bool>> Add(T data)
        {
            var res = new ServiceResult<bool>();
            try
            {
                await baseRepository.AddAsync(data);
                res.Message = "Add successed!";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            res.Success = true;
            return res;
        }

        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        public async Task<ServiceResult<bool>> Delete(string id)
        {
            var res = new ServiceResult<bool>();
            try
            {
                await baseRepository.DeleteAsync(id);
                res.Message = "Delete successed!";
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                res.Success = false;
            }
            res.Success = true;
            return res;
        }

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng nghiệp vụ</returns>
        public async Task<ServiceResult<IEnumerable<T>>> GetAll()
        {
            var res = new ServiceResult<IEnumerable<T>>();
            res.Success = true;
            res.Data = await baseRepository.GetAllAsync();
            return res;
        }

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Đối tượng nghiệp vụ</returns>
        public async Task<ServiceResult<T>> GetById(string id)
        {
            var res = new ServiceResult<T>();
            var data = await baseRepository.GetByIdAsync(id);
            if (data == null)
            {
                res.Success = false;
                res.Message = $"Not found any {MisaEntityHelper.GetTableName<T>()}";
            }
            res.Data = data;
            return res;
        }

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="data">Đối tượng cần cập nhật</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        public async Task<ServiceResult<bool>> Update(T data)
        {
            var res = new ServiceResult<bool>();
            try
            {
                await baseRepository.UpdateAsync(data);
                res.Message = "Update successed";
            }
            catch (Exception e)
            {
                res.Success = false;
                res.Message = e.Message;
            }
            res.Success = true;
            return res;
        }
    }
}
