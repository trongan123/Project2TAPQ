using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _service;

        public ProvinceController(IProvinceService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Province>> GetProvinces() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<Province> GetProvinceById(string id) => _service.FindProvinceById(id);



        [HttpPost]
        public IActionResult PortProvince(Province a)
        {
            _service.AddProvince(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProvince(string id)
        {
            var a = _service.FindProvinceById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteProvince(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProvince(string id, Province a)
        {
            var aTmp = _service.FindProvinceById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateProvince(a);
            return NoContent();
        }
    }
}
