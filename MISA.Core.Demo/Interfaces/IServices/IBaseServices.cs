using MISA.Core.Demo.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Interfaces.IServices
{
    /// <summary>
    /// Interface định nghĩa các nghiệp vụ cơ bản cho Service
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng nghiệp vụ</typeparam>
    public interface IBaseServices<T>
    {
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="data">Đối tượng cần thêm</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        Task<ServiceResult<bool>> Add(T data);

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="data">Đối tượng cần cập nhật</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        Task<ServiceResult<bool>> Update(T data);

        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Kết quả xử lý nghiệp vụ</returns>
        Task<ServiceResult<bool>> Delete(string id);

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng nghiệp vụ</returns>
        Task<ServiceResult<IEnumerable<T>>> GetAll();

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Đối tượng nghiệp vụ</returns>
        Task<ServiceResult<T>> GetById(string id);
    }
}
