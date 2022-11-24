using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemStoreHouseController : ControllerBase
    {
        private readonly IItemStoreHouseService _service;

        public ItemStoreHouseController(IItemStoreHouseService service)
        {
            this._service = service;
        }

        [HttpGet("ids")]
        public ActionResult<IEnumerable<ItemStoreHouse>> GetItemStoreHouses(string ids) => _service.getAll(ids);

        [HttpGet("id")]
        public ActionResult<ItemStoreHouse> GetItemStoreHouseById(string id) => _service.FindItemStoreHouseById(id);

        [HttpPost]
        public IActionResult PortItemStoreHouse(ItemStoreHouse a)
        {
            _service.AddItemStoreHouse(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteItemStoreHouse(string id)
        {
            var a = _service.FindItemStoreHouseById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteItemStoreHouse(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateItemStoreHouse(string id, ItemStoreHouse a)
        {
            var aTmp = _service.FindItemStoreHouseById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateItemStoreHouse(a);
            return NoContent();
        }
    }
}
