using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondController : ControllerBase
    {
        private readonly IPondService _service;

        public PondController(IPondService service)
        {
            this._service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pond>> getAllList() => _service.getAllList();

        [HttpGet("idacc")]
        public ActionResult<IEnumerable<Pond>> GetPonds(string idacc) => _service.getAll(idacc);
     
        [HttpGet("id")]
        public ActionResult<Pond> GetPondById(string id) => _service.FindPondById(id);

        [HttpGet("idRoom")]
        public ActionResult<IEnumerable<Pond>> getAllForCoo(string idRoom) => _service.getAllForCoo(idRoom);

        [HttpPost]
        public IActionResult PortPond(Pond a)
        {
            _service.AddPond(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeletePond(string id)
        {
            var a = _service.FindPondById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeletePond(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdatePond(string id, Pond a)
        {
            var aTmp = _service.FindPondById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdatePond(a);
            return NoContent();
        }
    }
}
