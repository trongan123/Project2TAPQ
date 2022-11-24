using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperativeRoomController : ControllerBase
    {
        private readonly ICooperativeRoomService _service;

        public CooperativeRoomController(ICooperativeRoomService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CooperativeRoom>> GetCooperativeRooms() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<CooperativeRoom> GetCooperativeRoomById(string id) => _service.FindCooperativeRoomById(id);

        [HttpGet("idacc")]
        public ActionResult<CooperativeRoom> GetCooperativeRoomByAccount(string idacc) => _service.GetCooperativeRoomByAccount(idacc);
      
        [HttpGet("code")]
        public ActionResult<CooperativeRoom> FindCooperativeRoomByCode(string code) => _service.FindCooperativeRoomByCode(code);
        [HttpPost]
        public IActionResult PortCooperativeRoom(CooperativeRoom a)
        {
            _service.AddCooperativeRoom(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteCooperativeRoom(string id)
        {
            var a = _service.FindCooperativeRoomById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteCooperativeRoom(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateCooperativeRoom(string id, CooperativeRoom a)
        {
            var aTmp = _service.FindCooperativeRoomById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateCooperativeRoom(a);
            return NoContent();
        }
    }
}
