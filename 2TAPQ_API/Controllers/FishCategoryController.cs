using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishCategoryController : ControllerBase
    {
        private readonly IFishCategoryService _service;

        public FishCategoryController(IFishCategoryService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FishCategory>> GetFishCategorys() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<FishCategory> GetFishCategoryById(string id) => _service.FindFishCategoryById(id);

        [HttpGet("status")]
        public ActionResult<IEnumerable<FishCategory>> getAllbyStatus(int status) => _service.getAllbyStatus(status);

        [HttpPost]
        public IActionResult PortFishCategory(FishCategory a)
        {
            _service.AddFishCategory(a);
            return NoContent();
        }

        [HttpDelete("idf")]
        public IActionResult ConfirmFishCategory(string idf)
        {
            var a = _service.FindFishCategoryById(idf);
            if (a == null)
            {
                return NotFound();
            }
            _service.ConfirmFishCategory(a);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult DeleteFishCategory(string id)
        {
            var a = _service.FindFishCategoryById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteFishCategory(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateFishCategory(string id, FishCategory a)
        {
            var aTmp = _service.FindFishCategoryById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateFishCategory(a);
            return NoContent();
        }
    }
}
