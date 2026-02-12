using MISA.Core.Demo;
using MISA.Core.Demo.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Demo.Infrastucture.Repositories
{
    public class CustomerRepo : BaseRepository<Customer>, ICustomerRepo
    {
        public CustomerRepo(IDbConnection connection) : base(connection)
        {
        }
    }
}
