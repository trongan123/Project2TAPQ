using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly INotifyService _service;

        public NotifyController(INotifyService service)
        {
            this._service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Notify>> getAllList() => _service.getAllList();

        [HttpGet("idacc")]
        public ActionResult<IEnumerable<Notify>> GetNotifys(string idacc) => _service.getAll(idacc);

        [HttpGet("Type")]
        public ActionResult<IEnumerable<Notify>> getAllByType(string Type) => _service.getAllByType(Type);

        [HttpGet("id")]
        public ActionResult<Notify> GetNotifyById(string id) => _service.FindNotifyById(id);

        [HttpGet("idRoom")]
        public ActionResult<IEnumerable<Notify>> getAllForCoo(string idRoom) => _service.getAllForCoo(idRoom);

        [HttpPost]
        public IActionResult PortNotify(Notify a)
        {
            _service.AddNotify(a);
            return NoContent();
        }
      
        [HttpPost("idcoop")]
        public IActionResult AddNotifyCoop(string idcoop, Notify a)
        {
            _service.AddNotifyCoop(idcoop,a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteNotify(string id)
        {
            var a = _service.FindNotifyById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteNotify(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateNotify(string id, Notify a)
        {
            var aTmp = _service.FindNotifyById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateNotify(a);
            return NoContent();
        }
        [HttpPut]
        public IActionResult Readed(List<Notify> a)
        {         
            _service.Readed(a);
            return NoContent();
        }
       
    }
}
