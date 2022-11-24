using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryStoreHouseController : ControllerBase
    {
        private readonly IHistoryStoreHouseService _service;

        public HistoryStoreHouseController(IHistoryStoreHouseService service)
        {
            this._service = service;
        }

        [HttpGet("ids")]
        public ActionResult<IEnumerable<HistoryStoreHouse>> GetHistoryStoreHouses(string ids) => _service.getAll(ids);

        [HttpGet("id")]
        public ActionResult<HistoryStoreHouse> GetHistoryStoreHouseById(string id) => _service.FindHistoryStoreHouseById(id);



        [HttpPost]
        public IActionResult PortHistoryStoreHouse(HistoryStoreHouse a)
        {
            _service.AddHistoryStoreHouse(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteHistoryStoreHouse(string id)
        {
            var a = _service.FindHistoryStoreHouseById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteHistoryStoreHouse(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateHistoryStoreHouse(string id, HistoryStoreHouse a)
        {
            var aTmp = _service.FindHistoryStoreHouseById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateHistoryStoreHouse(a);
            return NoContent();
        }
    }
}
