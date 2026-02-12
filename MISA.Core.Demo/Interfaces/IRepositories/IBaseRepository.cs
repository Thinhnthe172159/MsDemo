using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Interfaces.IRepositories
{
    public interface IBaseRepository<T>
    {
        Task<int> DeleteAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(object id);

        Task<T?> GetByIdAsync(object id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
