using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuantityHouseController : ControllerBase
    {
        private readonly IQuantityHouseService _service;

        public QuantityHouseController(IQuantityHouseService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuantityHouse>> GetQuantityHouses() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<QuantityHouse> GetQuantityHouseById(string id) => _service.FindQuantityHouseById(id);



        [HttpPost]
        public IActionResult PortQuantityHouse(QuantityHouse a)
        {
            _service.AddQuantityHouse(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteQuantityHouse(string id)
        {
            var a = _service.FindQuantityHouseById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteQuantityHouse(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateQuantityHouse(string id, QuantityHouse a)
        {
            var aTmp = _service.FindQuantityHouseById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateQuantityHouse(a);
            return NoContent();
        }
    }
}
