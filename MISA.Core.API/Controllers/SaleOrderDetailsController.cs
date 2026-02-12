using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Demo.Entities;
using MISA.Core.Demo.Interfaces.IServices;

namespace MISA.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrderDetailsController : BaseController<SaleOrderDetail>
    {
        public SaleOrderDetailsController(IBaseServices<SaleOrderDetail> baseService) : base(baseService)
        {
        }
    }
}
