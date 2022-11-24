using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _service;

        public DistrictController(IDistrictService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<District>> GetDistricts() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<District> GetDistrictById(string id) => _service.FindDistrictById(id);



        [HttpPost]
        public IActionResult PortDistrict(District a)
        {
            _service.AddDistrict(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteDistrict(string id)
        {
            var a = _service.FindDistrictById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteDistrict(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateDistrict(string id, District a)
        {
            var aTmp = _service.FindDistrictById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateDistrict(a);
            return NoContent();
        }
    }
}
