using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailReceiptsPaymentController : ControllerBase
    {
        private readonly IDetailReceiptsPaymentService _service;

        public DetailReceiptsPaymentController(IDetailReceiptsPaymentService service)
        {
            this._service = service;
        }

        [HttpGet("{idRP}/{status}")]
        public ActionResult<IEnumerable<DetailReceiptsPayment>> GetDetailReceiptsPayments(string idRP, int status) => _service.getAll(idRP, status);

        [HttpGet("id")]
        public ActionResult<DetailReceiptsPayment> GetDetailReceiptsPaymentById(string id) => _service.FindDetailReceiptsPaymentById(id);



        [HttpPost]
        public IActionResult PortDetailReceiptsPayment(DetailReceiptsPayment a)
        {
            _service.AddDetailReceiptsPayment(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteDetailReceiptsPayment(string id)
        {
            var a = _service.FindDetailReceiptsPaymentById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteDetailReceiptsPayment(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateDetailReceiptsPayment(string id, DetailReceiptsPayment a)
        {
            var aTmp = _service.FindDetailReceiptsPaymentById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateDetailReceiptsPayment(a);
            return NoContent();
        }
    }
}
