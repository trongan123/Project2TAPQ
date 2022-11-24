using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembers() => _service.getAll();
        
        [HttpGet("st")]
        public ActionResult<IEnumerable<Member>> getAllByStatus(int st) => _service.getAllByStatus(st);

        [HttpGet("id")]
        public ActionResult<Member> GetMemberById(string id) => _service.FindMemberById(id);

        [HttpGet("idfarm")]
        public ActionResult<Member> GetMemberByIdacc(string idfarm) => _service.FindMemberByIdacc(idfarm);

        [HttpPut("idAcc")]
        public IActionResult ConfirmMember(string id,Member a)
        {
            var aTmp = _service.FindMemberById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.ConfirmMember(a);
            return NoContent();
        }
        

        [HttpGet("idRoom")]
        public ActionResult<IEnumerable<Member>> getAllMember(string idRoom) => _service.getAllMember(idRoom);

        [HttpPost]
        public IActionResult PortMember(Member a)
        {
            _service.AddMember(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteMember(string id)
        {
            var a = _service.FindMemberById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteMember(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateMember(string id, Member a)
        {
            var aTmp = _service.FindMemberById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateMember(a);
            return NoContent();
        }
    }
}
