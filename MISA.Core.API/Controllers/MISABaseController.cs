using Microsoft.AspNetCore.Mvc;
using MISA.Core.Demo.Dto;
using MISA.Core.Demo.Interfaces.IServices;

namespace MISA.Demo.API.Controllers
{
    /// <summary>
    /// Controller cơ sở dùng chung CRUD
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly IBaseServices<T> baseService;

        protected BaseController(IBaseServices<T> baseService)
        {
            this.baseService = baseService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var res = await baseService.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var res = await baseService.GetById(id);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] T data)
        {
            var res = await baseService.Add(data);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] T data)
        {
            var res = await baseService.Update(data);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var res = await baseService.Delete(id);
            return  res.Success? Ok(res): BadRequest(res);
        }
    }
}
