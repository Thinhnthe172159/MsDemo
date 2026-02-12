using Dapper;
using MISA.Core.Demo.Helpers;
using MISA.Core.Demo.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Demo.Infrastucture.Repositories
{
    /// <summary>
    /// Repository base cung cấp các thao tác CRUD dùng chung cho các entity
    /// </summary>
    /// <typeparam name="T">Kiểu entity</typeparam>
    public class BaseRepository<T> : IBaseRepository<T>
    {
        /// <summary>
        /// Kết nối database được inject từ bên ngoài
        /// </summary>
        protected readonly IDbConnection _connection;

        /// <summary>
        /// Khởi tạo BaseRepository
        /// </summary>
        /// <param name="connection">Đối tượng kết nối database</param>
        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Thêm mới một bản ghi vào database
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public async Task<int> DeleteAsync(T entity)
        {
            // Lấy danh sách cột và giá trị tương ứng từ entity
            // Bao gồm khóa chính, bỏ qua các giá trị null
            var columns = MisaEntityHelper.GetColumnValues(
                entity,
                includeKey: true,
                ignoreNull: true
            );

            // Tạo câu lệnh INSERT động dựa trên metadata của entity
            var sql = $@"
                INSERT INTO {MisaEntityHelper.GetTableName<T>()}
                ({string.Join(", ", columns.Select(c => c.ColumnName))})
                VALUES
                ({string.Join(", ", columns.Select(c => "@" + c.PropertyName))})
            ";

            return await _connection.ExecuteAsync(sql, entity);
        }

        /// <summary>
        /// Cập nhật thông tin một bản ghi
        /// </summary>
        /// <param name="entity">Entity chứa dữ liệu cần cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public async Task<int> UpdateAsync(T entity)
        {
            // Lấy các cột cần update (không bao gồm khóa chính)
            // Bỏ qua các giá trị null để tránh ghi đè không cần thiết
            var columns = MisaEntityHelper.GetColumnValues(
                entity,
                includeKey: false,
                ignoreNull: true
            );

            // Tạo câu lệnh UPDATE động
            var sql = $@"
                UPDATE {MisaEntityHelper.GetTableName<T>()}
                SET {string.Join(", ",
                    columns.Select(c => $"{c.ColumnName} = @{c.PropertyName}"))}
                WHERE {MisaEntityHelper.GetKeyColumn<T>()}
                    = @{MisaEntityHelper.GetKeyProperty<T>()}
            ";

            return await _connection.ExecuteAsync(sql, entity);
        }

        /// <summary>
        /// Xóa một bản ghi theo khóa chính
        /// </summary>
        /// <param name="id">Giá trị khóa chính</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public async Task<int> DeleteAsync(object id)
        {
            var sql = $@"
                DELETE FROM {MisaEntityHelper.GetTableName<T>()}
                WHERE {MisaEntityHelper.GetKeyColumn<T>()} = @Id
            ";

            return await _connection.ExecuteAsync(sql, new { Id = id });
        }

        /// <summary>
        /// Lấy một bản ghi theo khóa chính
        /// </summary>
        /// <param name="id">Giá trị khóa chính</param>
        /// <returns>Entity tìm được hoặc null nếu không tồn tại</returns>
        public async Task<T?> GetByIdAsync(object id)
        {
            var sql = $@"
                SELECT *
                FROM {MisaEntityHelper.GetTableName<T>()}
                WHERE {MisaEntityHelper.GetKeyColumn<T>()} = @Id
            ";

            return await _connection.QueryFirstOrDefaultAsync<T>(
                sql,
                new { Id = id }
            );
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu của entity
        /// </summary>
        /// <returns>Danh sách entity</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $@"
                SELECT *
                FROM {MisaEntityHelper.GetTableName<T>()}
            ";

            return await _connection.QueryAsync<T>(sql);
        }
    }
}
