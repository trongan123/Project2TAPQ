using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _service;

        public ItemCategoryController(IItemCategoryService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemCategory>> GetItemCategorys() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<ItemCategory> GetItemCategoryById(string id) => _service.FindItemCategoryById(id);



        [HttpPost]
        public IActionResult PortItemCategory(ItemCategory a)
        {
            _service.AddItemCategory(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteItemCategory(string id)
        {
            var a = _service.FindItemCategoryById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteItemCategory(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateItemCategory(string id, ItemCategory a)
        {
            var aTmp = _service.FindItemCategoryById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateItemCategory(a);
            return NoContent();
        }
    }
}
