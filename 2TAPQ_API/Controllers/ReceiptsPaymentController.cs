using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsPaymentController : ControllerBase
    {
        private readonly IReceiptsPaymentService _service;

        public ReceiptsPaymentController(IReceiptsPaymentService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReceiptsPayment>> GetReceiptsPayments() => _service.getAll();

        [HttpGet("{id}/{status}")]
        public ActionResult<IEnumerable<ReceiptsPayment>> getallbyStatus(string id,int status) => _service.getallbyStatus(id,status);
       
        [HttpGet("id")]
        public ActionResult<ReceiptsPayment> GetReceiptsPaymentById(string id) => _service.FindReceiptsPaymentById(id);

        [HttpGet("idacc")]
        public ActionResult<ReceiptsPayment> GetReceiptsPaymentByIdAcc(string idacc) => _service.FindReceiptsPaymentByIdAcc(idacc);

        [HttpPost]
        public IActionResult PortReceiptsPayment(ReceiptsPayment a)
        {
            _service.AddReceiptsPayment(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteReceiptsPayment(string id)
        {
            var a = _service.FindReceiptsPaymentById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteReceiptsPayment(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateReceiptsPayment(string id, ReceiptsPayment a)
        {
            var aTmp = _service.FindReceiptsPaymentById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateReceiptsPayment(a);
            return NoContent();
        }
    }
}
