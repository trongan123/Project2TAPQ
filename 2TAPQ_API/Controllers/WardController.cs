using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly IWardService _service;

        public WardController(IWardService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ward>> GetWards() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<Ward> GetWardById(string id) => _service.FindWardById(id);



        [HttpPost]
        public IActionResult PortWard(Ward a)
        {
            _service.AddWard(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteWard(string id)
        {
            var a = _service.FindWardById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteWard(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateWard(string id, Ward a)
        {
            var aTmp = _service.FindWardById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateWard(a);
            return NoContent();
        }
    }
}
