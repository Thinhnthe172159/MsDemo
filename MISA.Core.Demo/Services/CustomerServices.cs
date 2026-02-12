using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Interfaces.IRepositories;
using MISA.Core.Demo.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Services
{
    public class CustomerServices : BaseServices<Customer>, ICustomerServices
    {
        public CustomerServices(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
        }
    }
}
