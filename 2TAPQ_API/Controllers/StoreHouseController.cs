using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreHouseController : ControllerBase
    {
        private readonly IStoreHouseService _service;

        public StoreHouseController(IStoreHouseService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreHouse>> GetStoreHouses() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<StoreHouse> GetStoreHouseById(string id) => _service.FindStoreHouseById(id);

        [HttpGet("idacc")]
        public ActionResult<StoreHouse> FindStoreHouseByIdAcc(string idacc) => _service.FindStoreHouseByIdAcc(idacc);

        [HttpPost]
        public IActionResult PortStoreHouse(StoreHouse a)
        {
            _service.AddStoreHouse(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteStoreHouse(string id)
        {
            var a = _service.FindStoreHouseById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteStoreHouse(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateStoreHouse(string id, StoreHouse a)
        {
            var aTmp = _service.FindStoreHouseById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateStoreHouse(a);
            return NoContent();
        }
    }
}
