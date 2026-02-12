using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Demo;
using MISA.Core.Demo.Interfaces.IServices;

namespace MISA.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customers2Controller : BaseController<Customer>
    {
        public Customers2Controller(IBaseServices<Customer> baseService) : base(baseService)
        {
        }
    }
}
